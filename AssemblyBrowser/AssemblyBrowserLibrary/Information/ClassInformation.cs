using System;
using System.Collections.ObjectModel;

namespace AssemblyBrowserLibrary
{
    public class ClassInformation
    {
        private readonly Type _type;
        public string Name { get; }
        public ObservableCollection<ClassMemberInformation> Members { get; set; }
        public string FullName { get; }
        public ClassInformation(Type type, string name)
        {
            _type = type;
            Name = name;
            FullName = GetFullName();
            Members = new ObservableCollection<ClassMemberInformation>
            {
                new ClassMemberInformation("Fields"),
                new ClassMemberInformation("Properties"),
                new ClassMemberInformation("Methods")
            };
        }
        private string GetFullName()
        {
            return AccessModifier.GetClassAccessModifier(_type) + " " + Name;
        }
    }
}