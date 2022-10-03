using System.IO;

namespace ConsoleApp
{
    public static class PathWorker
    {
        public static void CreateDirectory(string filePath)
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        }
    }
}