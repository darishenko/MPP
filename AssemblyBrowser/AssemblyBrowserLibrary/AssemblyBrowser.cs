using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserLibrary
{
    public static class AssemblyBrowser
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