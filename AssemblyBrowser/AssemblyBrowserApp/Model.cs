using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyBrowserLibrary;

namespace AssemblyBrowser
{
    internal class Model : INotifyPropertyChanged
    {
        private ObservableCollection<AssemblyInformation> _assemblyData;
        private string _assemblyFileName;

        public string AssemblyFileName
        {
            get => _assemblyFileName;
            set
            {
                _assemblyFileName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AssemblyInformation> AssemblyData
        {
            get => _assemblyData;
            set
            {
                _assemblyData = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}