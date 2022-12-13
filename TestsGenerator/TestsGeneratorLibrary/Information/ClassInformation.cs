using System.Collections.Generic;

namespace TestsGenerator
{
    public class ClassInformation
    {
        private List<string> methods;

        public ClassInformation(string name, string nameSpace, List<string> methods)
        {
            Name = name;
            NamespaceNameSpace = nameSpace;
            this.methods = new List<string>(methods);
        }

        public string Name { get; }
        public string NamespaceNameSpace { get; }
        
        public List<string> Methods
        {
            get => methods;
            set => methods = new List<string>(value);
        }
    }
}