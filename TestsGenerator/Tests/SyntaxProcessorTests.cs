using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsGenerator.Syntax;

namespace TestsGenerator.Tests
{
    [TestClass]
    public class SyntaxProcessorTests
    {
        private AsyncFileReader asyncReader;
        private string pathToFile;
        private SyntaxNode root;
        private SyntaxProcessor syntaxProcessor;
        private SyntaxProcessResult syntaxProcessResult;

        [TestInitialize]
        public void SetUp()
        {
            pathToFile = Environment.CurrentDirectory + "\\TestSource\\Class.cs";
            asyncReader = new AsyncFileReader();
            var sourceCode = asyncReader.Read(pathToFile).Result;

            syntaxProcessor = new SyntaxProcessor();
            syntaxProcessResult = syntaxProcessor.Process(sourceCode);

            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            root = syntaxTree.GetRoot();
        }

        [TestMethod]
        public void ProcessTest()
        {
            var classes = syntaxProcessResult.Classes;
            foreach (var cls in classes)
            {
                var sameClasses = new List<ClassDeclarationSyntax>(root.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Where(classInfo =>
                        classInfo.Identifier.ToString() == cls.Name
                        && ((NamespaceDeclarationSyntax) classInfo.Parent).Name.ToString() == cls.NamespaceNameSpace));
                Assert.AreNotEqual(sameClasses.Count, 0);
            }
        }
    }
}