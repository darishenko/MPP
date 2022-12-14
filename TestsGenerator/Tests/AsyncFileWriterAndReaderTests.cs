using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsGenerator;

namespace UnitTests
{
    [TestClass]
    public class AsyncFileWriterAndReaderTests
    {
        private AsyncFileReader asyncReader;
        private AsyncFileWriter asyncWriter;
        private string fileName;
        private string pathToDirectory;
        private string pathToFile;
        private List<TestClassInformation> information;
        private string testString;

        [TestInitialize]
        public void SetUp()
        {
            pathToFile = Environment.CurrentDirectory + "\\TestResults\\Test.cs";
            pathToDirectory = Environment.CurrentDirectory + "\\TestResults";
            asyncReader = new AsyncFileReader();
            asyncWriter = new AsyncFileWriter(pathToDirectory);
            fileName = "Test.cs";
            testString = "Write and read test!";
            information = new List<TestClassInformation>();
            information.Add(new TestClassInformation(fileName, testString));
        }

        [TestMethod]
        public void WriteAndReadTest()
        {
            asyncWriter.Write(information).Wait();
            var resultStr = asyncReader.Read(pathToFile).Result;
            Assert.IsNotNull(resultStr);
            Assert.AreEqual(testString, resultStr);
            File.Delete(pathToFile);
        }
    }
}