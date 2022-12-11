using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AssemblyBrowserLibrary;
using Microsoft.Win32;

namespace AssemblyBrowser
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private readonly Model _model;
        private ICommand _openFileCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel(Model model)
        {
            _model = model;
        }

        public string AssemblyFileName
        {
            get => _model.AssemblyFileName;
            set
            {
                _model.AssemblyFileName = value;
                AssemblyData = new ObservableCollection<AssemblyInformation>
                {
                    AssemblyBrowserLibrary.AssemblyBrowser.GetAssemblyInformation(_model.AssemblyFileName)
                };
                
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AssemblyInformation> AssemblyData
        {
            get => _model.AssemblyData;
            set
            {
                _model.AssemblyData = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ??= new Command(obj =>
                {
                    var dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == true) 
                        AssemblyFileName = dialog.FileName;
                });
            }
        }
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}