using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator
{
    public class AsyncFileWriter
    {
        private readonly string outputDirectory;

        public AsyncFileWriter(string outPutDirectory)
        {
            outputDirectory = outPutDirectory;
        }

        public async Task Write(List<TestClassInformation> classesInformation)
        {
            if (classesInformation == null) return;

            foreach (var classInformation in classesInformation)
            {
                var filePath = outputDirectory + "\\" + classInformation.FileName;
                using (var writer = new StreamWriter(filePath))
                {
                    await writer.WriteAsync(classInformation.InnerText);
                }   
            }

        }
    }
}