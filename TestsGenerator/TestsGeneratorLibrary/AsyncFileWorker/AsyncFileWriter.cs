using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator
{
    public class AsyncFileWriter
    {
        private string outputDirectory;

        public AsyncFileWriter(string outPutDirectory)
        {
            this.outputDirectory = outPutDirectory;
        }

        public async Task Write(TestClassTemplate classTemplate)
        {
            if (classTemplate == null)
            {
                return;
            }

            string filePath = outputDirectory + "\\" + classTemplate.FileName;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(classTemplate.InnerText);
            }
        }
    }
}
