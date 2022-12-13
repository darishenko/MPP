using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TestsGenerator;

namespace TestsGeneratorApplication
{
    public partial class AppForm : Form
    {
        private const string ChoosingFilesTypes = "C# class files (*.cs) | *.cs";

        private const string PathToSaveFiles =
            @"C:\Users\Darishenko\Darishenko\UNIVERSITY\5 Semester\SPP\Labs\TestsGeneratorLibrary\TestDirectory\";

        private const string PathToChooseFiles =
            @"C:\Users\Darishenko\Darishenko\UNIVERSITY\5 Semester\SPP\Labs\AssemblyBrowser\AssemblyBrowserLibrary";

        private readonly List<string> files;

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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void NumbersOnly_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            if (Regex.IsMatch(textBox.Text, "[^0-9]")) textBox.Text = "";
        }

        private void AddFilesToList(string[] fileNames)
        {
            foreach (var file in fileNames)
                if (!files.Contains(file))
                    files.Add(file);
        }

        private void btnChooseFiles_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ChoosingFilesTypes;
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = PathToChooseFiles;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) AddFilesToList(openFileDialog.FileNames);
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (tbMaxRead.Text.Length == 0 || tbMaxProcess.Text.Length == 0 || tbMaxWrite.Text.Length == 0)
            {
                MessageBox.Show("Please, input initial parameters");
                return;
            }

            var maxReadFilesCount = Convert.ToInt32(tbMaxRead.Text);
            var maxProcessTasksCount = Convert.ToInt32(tbMaxProcess.Text);
            var maxWriteFilesCount = Convert.ToInt32(tbMaxWrite.Text);

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

            var generator = new TestGenerator(files, maxReadFilesCount, maxProcessTasksCount, maxWriteFilesCount);

            string folderPath;
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = PathToSaveFiles;
                var result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    folderPath = fbd.SelectedPath;
                else
                    return;
            }

            if (!string.IsNullOrEmpty(folderPath))
            {
                var asyncWriter = new AsyncFileWriter(folderPath);
                await generator.Generate(asyncWriter);
            }
        }
    }
}