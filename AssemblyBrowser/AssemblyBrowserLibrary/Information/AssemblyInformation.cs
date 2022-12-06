using System.Collections.ObjectModel;

namespace AssemblyBrowserLibrary
{
    public class AssemblyInformation
    {
        public string Name { get; }
        public AssemblyInformation(string name)
        {
            Name = name;
            Namespaces = new ObservableCollection<NamespaceInformation>();
        }
        public ObservableCollection<NamespaceInformation> Namespaces { get;}
    }
}