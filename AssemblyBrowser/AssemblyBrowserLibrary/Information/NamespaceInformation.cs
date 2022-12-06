using System.Collections.ObjectModel;

namespace AssemblyBrowserLibrary
{
    public class NamespaceInformation
    {
        public NamespaceInformation(string name)
        {
            Name = name;
            Classes = new ObservableCollection<ClassInformation>();
        }
        public string Name { get; }
        public ObservableCollection<ClassInformation> Classes { get;}
    }
}