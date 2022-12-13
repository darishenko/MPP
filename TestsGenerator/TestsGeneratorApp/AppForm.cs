using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestsGenerator;

namespace TestsGeneratorApplication
{
    public partial class AppForm : Form
    {
        private List<string> files;
        private const string ChoosingFilesTypes = "C# class files (*.cs) | *.cs";
        private const string PathToSaveFiles =@"C:\Users\Darishenko\Darishenko\UNIVERSITY\5 Semester\SPP\Labs\TestsGeneratorLibrary\TestDirectory\";
        private const string PathToChooseFiles = @"C:\Users\Darishenko\Darishenko\UNIVERSITY\5 Semester\SPP\Labs\AssemblyBrowser\AssemblyBrowserLibrary";
        
        public AppForm()
        {
            files = new List<string>();
            Width = 650;
            Height = 350;
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        
        private void NumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        private void NumbersOnly_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                textBox.Text = "";
            }
        }
        
        private void AddFilesToList(string[] fileNames)
        {
            foreach (string file in fileNames)
            {
                if (!files.Contains(file))
                {
                    files.Add(file);
                }
            }
        }
        
        private void btnChooseFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ChoosingFilesTypes;
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = PathToChooseFiles;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                AddFilesToList(openFileDialog.FileNames);
            }
        }
        
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (tbMaxRead.Text.Length == 0 || tbMaxProcess.Text.Length == 0 || tbMaxWrite.Text.Length == 0)
            {
                MessageBox.Show("Please, input initial parameters");
                return;
            }

            int maxReadFilesCount = Convert.ToInt32(tbMaxRead.Text);
            int maxProcessTasksCount = Convert.ToInt32(tbMaxProcess.Text);
            int maxWriteFilesCount = Convert.ToInt32(tbMaxWrite.Text);

            if (!(maxReadFilesCount > 0 && maxProcessTasksCount > 0 && maxWriteFilesCount > 0))
            {
                MessageBox.Show("Bad initial parameters");
                return;
            }

            if (files.Count == 0)
            {
                MessageBox.Show("List of files is empty. Choose minimum one file.");
                return;
            }

            TestsGenerator.TestGenerator generator = new TestsGenerator.TestGenerator(files, maxReadFilesCount, maxProcessTasksCount, maxWriteFilesCount);

            string folderPath;
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = PathToSaveFiles;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folderPath = fbd.SelectedPath;
                }
                else
                {
                    return;
                }
            }
            
            if (!string.IsNullOrEmpty(folderPath))
            {
                AsyncFileWriter asyncWriter = new AsyncFileWriter(folderPath);
                await generator.Generate(asyncWriter);
            }
        }
        
    }
}
