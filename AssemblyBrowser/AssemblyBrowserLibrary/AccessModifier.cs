using System;
using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public static class AccessModifier
    {
        public static string GetMethodAccessModifier(MethodInfo method)
        {
            var result = "";
            if (method.IsPublic) result += "public";
            else if (method.IsPrivate) result += "private";
            else if (method.IsFamily) result += "protected";
            else if (method.IsAssembly) result += "internal";
            else if (method.IsFamilyOrAssembly) result += "protected internal";
            
            if (method.IsStatic) result += " static";
            else if (method.IsAbstract) result += " abstract";
            else if (method.IsVirtual) result += " virtual";
            else if (method.GetBaseDefinition() != method) result += " override";
            
            return result;
        }

        public static string GetClassAccessModifier(Type type)
        {
            var result = "";
            
            if (type.IsPublic) result += "public";
            else if (type.IsNestedPrivate) result += "private";
            else if (type.IsNestedFamily) result += "protected";
            else if (type.IsNestedAssembly) result += "internal";
            else if (type.IsNestedFamANDAssem) result += "protected internal";
            else if (type.IsNotPublic) result += "private";
            
            if (type.IsAbstract && type.IsSealed) result += " static";
            else if (type.IsAbstract && !type.IsInterface) result += " abstract";
            
            if (type.IsClass) result += " class";
            if (type.IsEnum) result += " enum";
            if (type.IsInterface) result += " interface";
            if (type.IsValueType && type.IsPrimitive) result += " struct";
            
            return result;
        }

        public static string GetFieldAccessModifier(FieldInfo field)
        {
            if (field == null) return "";

            var result = "";
            if (field.IsPublic) result += "public";
            else if (field.IsPrivate) result += "private";
            else if (field.IsFamily) result += "protected";
            else if (field.IsAssembly) result += "internal";
            else if (field.IsFamilyOrAssembly) result += "protected internal";
            
            if (field.IsStatic) result += " static";
            
            return result;
        }

        public static string GetPropertyAccessModifier(PropertyInfo prop)
        {
            if (prop == null) return "";
            
            var result = "";
            
            if (prop.CanRead) result += "public";
            else result += "private ";
            
            result += " get; ";
            
            if (prop.CanWrite) result += "public";
            else result += "private";
            
            result += " set;";
            
            return result;
        }
    }
}