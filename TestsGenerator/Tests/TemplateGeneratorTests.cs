using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsGenerator;

namespace Tests
{
    [TestClass]
    public class TemplateGeneratorTests
    {
        private AsyncFileReader asyncReader;
        private SyntaxNode generatedRoot;
        private string pathToFile;
        private string sourceCode;
        private SyntaxNode sourceRoot;
        private TemplateClassGenerator templateGenerator;
        private List<TestClassInformation> testClassesInformation;

        [TestInitialize]
        public void SetUp()
        {
            pathToFile = Environment.CurrentDirectory + "\\TestSource\\Class.cs";
            asyncReader = new AsyncFileReader();
            sourceCode = asyncReader.Read(pathToFile).Result;
            templateGenerator = new TemplateClassGenerator();
            sourceRoot = CSharpSyntaxTree.ParseText(sourceCode).GetRoot();
        }

        [TestMethod]
        public void GetTemplateTest()
        {
            testClassesInformation = new List<TestClassInformation>(templateGenerator.GetTemplate(sourceCode));
            Assert.IsNotNull(testClassesInformation);

            foreach (var testClassInformation in testClassesInformation)
            {
                generatedRoot = CSharpSyntaxTree.ParseText(testClassInformation.InnerText).GetRoot();
                var generatedClasses =
                    new List<ClassDeclarationSyntax>(generatedRoot.DescendantNodes().OfType<ClassDeclarationSyntax>());
                var sourceClasses =
                    new List<ClassDeclarationSyntax>(sourceRoot.DescendantNodes().OfType<ClassDeclarationSyntax>());
                Assert.AreEqual(sourceClasses.Count, generatedClasses.Count);

                for (var i = 0; i < sourceClasses.Count; i++)
                {
                    var sourceClass = sourceClasses[i];
                    var generatedClass = generatedClasses[i];

                    var sourceMethods = new List<MethodDeclarationSyntax>(
                        sourceClass.DescendantNodes().OfType<MethodDeclarationSyntax>()
                            .Where(method => method.Modifiers.Any(modifer => modifer.ToString() == "public")));
                    var generatedMethods = new List<MethodDeclarationSyntax>(
                        generatedClass.DescendantNodes().OfType<MethodDeclarationSyntax>()
                            .Where(method => method.Modifiers.Any(modifer => modifer.ToString() == "public")));
                    Assert.AreEqual(sourceMethods.Count, generatedMethods.Count);

                    for (var j = 0; j < sourceMethods.Count; j++)
                    {
                        var sourceMethod = sourceMethods[j];
                        var generatedMethod = generatedMethods[j];
                        Assert.IsTrue(generatedMethod.Identifier.ToString().Contains(sourceMethod.Identifier.ToString()));
                    }
                }
            }
        }
    }
}