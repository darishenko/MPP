namespace TestsGenerator
{
    public class TestClassInformation
    {
        public string FileName { get; }
        public string InnerText { get; }
        
        public TestClassInformation(string fileName, string innerText)
        {
            FileName = fileName;
            InnerText = innerText;
        }
    }
}