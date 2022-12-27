using System;
using System.Collections.Generic;
using System.Reflection;
using FieldCreators;
using System.IO;

namespace FakerLib
{
    public class Faker : IFaker
    {
        private Dictionary<Type, IPrimitiveTypeCreator> _primitiveTypeCreator;
        private Dictionary<Type, IGenericCreator> _genericTypeCreator;

        private Dictionary<PropertyInfo, IPrimitiveTypeCreator> _customPrimitiveTypeCreator =
            new Dictionary<PropertyInfo, IPrimitiveTypeCreator>();

        private HashSet<Type> _createdTypesInClass;

        public Faker()
        {
            _primitiveTypeCreator = PrimitiveTypesCreator.GetPrimitiveTypes();
            _createdTypesInClass = new HashSet<Type>();
            _genericTypeCreator = new Dictionary<Type, IGenericCreator>();
            var assemblies = new List<Assembly>();
            var path = @"C:\Users\Darishenko\Darishenko\UNIVERSITY\5 Semester\SPP\Labs\Faker\ListCreatorLib\bin\Debug";

            try
            {
                foreach (var file in Directory.GetFiles(path, "*.dll"))
                    try
                    {
                        assemblies.Add(Assembly.LoadFile(file));
                    }
                    catch (BadImageFormatException)
                    {
                    }
                    catch (FileLoadException)
                    {
                    }
            }
            catch (DirectoryNotFoundException)
            {
            }

            foreach (var assembly in assemblies)
            foreach (var type in assembly.GetTypes())
            foreach (var typeInterface in type.GetInterfaces())
                if (typeInterface == typeof(IGenericCreator))
                {
                    var creator = (IGenericCreator) Activator.CreateInstance(type, _primitiveTypeCreator);
                    _genericTypeCreator.Add(creator.CurType, creator);
                }
                else if (typeInterface == typeof(IPrimitiveTypeCreator))
                {
                    var creator = (IPrimitiveTypeCreator) Activator.CreateInstance(type);
                    _primitiveTypeCreator.Add(creator.CurType, creator);
                }
        }

        public Faker(FakerConfig config) : this()
        {
            _customPrimitiveTypeCreator = config.creators;
        }

        public T Create<T>()
        {
            return (T) CreateObject(typeof(T));
        }

        private object CreateObject(Type type)
        {
            object createdObject = null;
            
            if (_primitiveTypeCreator.TryGetValue(type, out var creator))
            {
                createdObject = creator.Create();
            }
            else if (type.IsValueType)
            {
                createdObject = Activator.CreateInstance(type);
            }
            else if (type.IsGenericType &&
                     _genericTypeCreator.TryGetValue(type.GetGenericTypeDefinition(), out var genCreator))
            {
                createdObject = genCreator.Create(type.GenericTypeArguments[0]); //type of object in collection
            }
            else if (type.IsClass && !type.IsArray && !type.IsPointer && !type.IsAbstract && !type.IsGenericType)
            {
                if (!_createdTypesInClass.Contains(type))
                    createdObject = CreateClass(type);
                else
                    createdObject = null;
            }

            return createdObject;
        }


        public object CreateClass(Type type)
        {
            object createdClass = null;

            var largestConstructor = 0;
            ConstructorInfo constructor = null;
            var constructorsOfClass =
                type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var curConstructor in constructorsOfClass)
            {
                var curCount = curConstructor.GetParameters().Length;
                if (curCount > largestConstructor)
                {
                    largestConstructor = curCount;
                    constructor = curConstructor;
                }
            }

            _createdTypesInClass.Add(type);

            if (constructor != null) createdClass = CreateFromConstructor(constructor, type);

            createdClass = CreateFromProperties(type, createdClass);

            _createdTypesInClass.Remove(type);

            return createdClass;
        }


        private object CreateFromProperties(Type type, object createdObject)
        {
            object created = null;
            if (createdObject == null)
                created = Activator.CreateInstance(type);
            else
                created = createdObject;

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static |
                                                     BindingFlags.NonPublic))
                if (fieldInfo.GetValue(created) == null)
                {
                    object value = null;
                    if (!CreateByCustomCreator(fieldInfo, out value)) 
                        value = CreateObject(fieldInfo.FieldType);
                    fieldInfo.SetValue(created, value);
                }

            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                            BindingFlags.Static | BindingFlags.NonPublic))
                if (propertyInfo.CanWrite)
                    if (propertyInfo.GetValue(created) == null)
                    {
                        object value = null;
                        if (!CreateByCustomCreator(propertyInfo, out value))
                            value = CreateObject(propertyInfo.PropertyType);
                        propertyInfo.SetValue(created, value);
                    }

            return created;
        }

        private object CreateFromConstructor(ConstructorInfo constructor, Type type)
        {
            var parametersValues = new List<object>();

            foreach (var parameterInfo in constructor.GetParameters())
            {
                object value = null;
                if (!CreateByCustomCreator(parameterInfo, type, out value))
                    value = CreateObject(parameterInfo.ParameterType);
                parametersValues.Add(value);
            }

            try
            {
                return constructor.Invoke(parametersValues.ToArray());
            }
            catch (TargetInvocationException)
            {
                return null;
            }
        }
        
        private bool CreateByCustomCreator(ParameterInfo parameterInfo, Type type, out object created)
        {
            foreach (var keyValue in _customPrimitiveTypeCreator)
                if (keyValue.Key.Name == parameterInfo.Name &&
                    keyValue.Value.CurType == parameterInfo.ParameterType &&
                    keyValue.Key.ReflectedType == type)
                {
                    created = keyValue.Value.Create();
                    return true;
                }

            created = null;
            return false;
        }

        private bool CreateByCustomCreator(PropertyInfo propertyInfo, out object created)
        {
            if (_customPrimitiveTypeCreator.TryGetValue(propertyInfo, out var creator))
            {
                created = creator.Create();
                return true;
            }
            else
            {
                created = null;
                return false;
            }
        }

        private bool CreateByCustomCreator(FieldInfo fieldInfo, out object created)
        {
            foreach (var keyValue in _customPrimitiveTypeCreator)
                if (keyValue.Key.Name == fieldInfo.Name && keyValue.Value.CurType == fieldInfo.FieldType &&
                    keyValue.Key.ReflectedType == fieldInfo.ReflectedType)
                {
                    created = keyValue.Value.Create();
                    return true;
                }

            created = null;
            return false;
        }
    }
    
    public class AssemblyBrowser
    {
        public static AssemblyInformation GetAssemblyInformation(string path)
        {
            var assembly = Assembly.LoadFrom(path);
            var assemblyInformation = new AssemblyInformation(assembly.GetName().Name);
            
            var namespaces = assembly.GetTypes().Select(type => type.Namespace).Distinct().ToList()
                .Where(nameSpace => nameSpace != null).ToList();
            
            namespaces.ForEach(nameSpace =>
            {
                var namespaceInformation = new NamespaceInformation(nameSpace);
                assemblyInformation.Namespaces.Add(namespaceInformation);

                var classes = assembly.GetTypes()
                    .Where(type => type.IsClass && type.Namespace == nameSpace).ToList();

                classes.ForEach(clazz =>
                {
                    var classInformation = new ClassInformation(clazz, clazz.Name);
                    namespaceInformation.Classes.Add(classInformation);

                    var fields = clazz.GetFields(
                        BindingFlags.NonPublic | 
                        BindingFlags.Public | 
                        BindingFlags.Static |
                        BindingFlags.Instance).ToList();
                    
                    var properties = clazz.GetProperties(
                        BindingFlags.NonPublic | 
                        BindingFlags.Public |
                        BindingFlags.Static | 
                        BindingFlags.Instance).ToList();
                    
                    var methods = clazz.GetMethods(
                            BindingFlags.NonPublic | 
                            BindingFlags.Public | 
                            BindingFlags.Static |
                            BindingFlags.Instance)
                        .Where(method => !method.IsDefined(typeof(ExtensionAttribute))).ToList();

                    fields.ForEach(field =>
                        classInformation.Members.First(memberType => memberType.Name == "Fields")
                            .AddField(field));
                    
                    properties.ForEach(
                        property => classInformation.Members.First(memberType => memberType.Name == "Properties")
                            .AddProperty(property));
                    
                    methods.ForEach(method =>
                        classInformation.Members.First(memberType => memberType.Name == "Methods")
                            .AddMethod(method, false));
                    
                });

                classes.ForEach(clazz =>
                {
                    clazz.GetMethods().Where(method => method.IsDefined(typeof(ExtensionAttribute), false)).ToList()
                        .ForEach(parameter =>
                        {
                            assemblyInformation.Namespaces.ToList().ForEach(nameSpace =>
                                nameSpace.Classes.First(clazz =>
                                        clazz.Name == parameter.GetParameters()[0].ParameterType.Name)
                                    ?.Members.First(method => method.Name == "Methods")
                                    .AddMethod(parameter, true));
                        });
                });
                
            });

            return assemblyInformation;
        }
    }
}