using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsGenerator;

namespace Tests
{
    [TestClass]
    public class TestsGeneratorTests
    {
        private AsyncFileWriter asyncFileWriter;
        private List<string> files;
        private int maxProcessableCount;
        private int maxReadableCount;
        private int maxWritableCount;
        private string resultPath;
        private TestGenerator testsGenerator;

        [TestInitialize]
        public void SetUp()
        {
            resultPath = Environment.CurrentDirectory + "\\TestResults";
            asyncFileWriter = new AsyncFileWriter(resultPath);

            files = new List<string>();
            var pathToFiles = Environment.CurrentDirectory + "\\TestSource\\Class.cs";
            files.Add(pathToFiles);

            maxReadableCount = 3;
            maxProcessableCount = 3;
            maxWritableCount = 3;
            
            testsGenerator = new TestGenerator(files, maxReadableCount, maxProcessableCount, maxWritableCount);
        }

        [TestMethod]
        public void GenerateTest()
        {
            var prevCountOfFiles = Directory.GetFiles(resultPath).Length;
            testsGenerator.Generate(asyncFileWriter).Wait();
            var currentCountOfFiles = Directory.GetFiles(resultPath).Length;
            var expectedCount = 1+ files.Count - prevCountOfFiles;
            Assert.AreEqual(expectedCount, currentCountOfFiles);
            foreach (var filePath in files)
            {
                var pathToResFile = resultPath + "\\" + Path.GetFileNameWithoutExtension(filePath) + "Tests.cs";
                File.Delete(pathToResFile);
            }
        }
    }
}