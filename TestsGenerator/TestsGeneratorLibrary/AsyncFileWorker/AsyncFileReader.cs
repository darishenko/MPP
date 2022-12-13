using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator
{
    public class AsyncFileReader
    {
        public async Task<string> Read(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}