using System.Collections.Generic;

namespace TestsGenerator.Syntax
{
    public class SyntaxProcessResult
    {
        private List<ClassInformation> classes;

        public SyntaxProcessResult(List<ClassInformation> classes)
        {
            Classes = classes;
        }

        public List<ClassInformation> Classes
        {
            get => classes;
            set => classes = new List<ClassInformation>(value);
        }
    }
}