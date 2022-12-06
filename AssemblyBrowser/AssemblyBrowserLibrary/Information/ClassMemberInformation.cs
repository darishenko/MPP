using System.Collections.ObjectModel;
using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public class ClassMemberInformation
    {
        
        public string Name { get; }
        public ObservableCollection<string> ClassMembers { get; }
        public ClassMemberInformation(string name)
        {
            Name = name;
            ClassMembers = new ObservableCollection<string>();
        }
        public void AddField(FieldInfo fieldInfo)
        {
            var fullName = AccessModifier.GetFieldAccessModifier(fieldInfo) + " " + fieldInfo;
            ClassMembers.Add(fullName);
        }

        public void AddMethod(MethodInfo methodInfo, bool isExtension)
        {
            var fullName = "";
            if (isExtension) fullName = "EXTENSION: ";
            fullName += AccessModifier.GetMethodAccessModifier(methodInfo) + " " + methodInfo;
            ClassMembers.Add(fullName);
        }

        public void AddProperty(PropertyInfo propertyInfo)
        {
            var fullName = AccessModifier.GetPropertyAccessModifier(propertyInfo) + " " + propertyInfo;
            ClassMembers.Add(fullName);
        }
    }
}