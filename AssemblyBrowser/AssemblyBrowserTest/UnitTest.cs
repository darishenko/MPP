using AssemblyBrowserLibrary;
using NUnit.Framework;

namespace AssemblyBrowserTest
{
    public class Tests
    {
        private AssemblyInformation assemblyInformation;
        public int x;
        public string P { get; set; }

        [SetUp]
        public void Setup()
        {
            assemblyInformation = AssemblyBrowser.GetAssemblyInformation(@"..\netcoreapp3.1\AssemblyBrowserTest.dll");
        }

        [Test]
        public void TestAssemblyBrowser()
        {
            Assert.AreEqual(assemblyInformation.Name, "AssemblyBrowserTest");
            Assert.AreEqual(assemblyInformation.Namespaces.Count, 1);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Name, "AssemblyBrowserTest");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes.Count, 2);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Name, "ClassWithExtensionMethod");

            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members.Count, 3);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[0].Name, "Fields");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[1].Name, "Properties");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[2].Name, "Methods");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[0].ClassMembers.Count, 0);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[1].ClassMembers.Count, 0);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[2].ClassMembers.Count, 6);

            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Name, "Tests");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members.Count, 3);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[0].Name, "Fields");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[1].Name, "Properties");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[2].Name, "Methods");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[0].ClassMembers.Count, 3);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[1].ClassMembers.Count, 1);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[2].ClassMembers.Count, 15);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[2].ClassMembers[14],
                "EXTENSION: public static Int32 extensionMethod(AssemblyBrowserTest.Tests, Int32)");
        }

        [Test]
        public void TasteNamespaces()
        {
            Assert.AreEqual(assemblyInformation.Namespaces.Count, 1);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Name, "AssemblyBrowserTest");
        }

        [Test]
        public void TasteClasses()
        {
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes.Count, 2);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Name, "ClassWithExtensionMethod");
        }

        [Test]
        public void TestClassesMembers()
        {
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Name, "ClassWithExtensionMethod");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members.Count, 3);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[0].Name, "Fields");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[1].Name, "Properties");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[2].Name, "Methods");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[0].ClassMembers.Count, 0);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[1].ClassMembers.Count, 0);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[0].Members[2].ClassMembers.Count, 6);
            
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Name, "Tests");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members.Count, 3);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[0].Name, "Fields");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[1].Name, "Properties");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[2].Name, "Methods");
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[0].ClassMembers.Count, 3);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[1].ClassMembers.Count, 1);
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[2].ClassMembers.Count, 15);
        }

        [Test]
        public void TestExtension()
        {
            Assert.AreEqual(assemblyInformation.Namespaces[0].Classes[1].Members[2].ClassMembers[14],
                "EXTENSION: public static Int32 extensionMethod(AssemblyBrowserTest.Tests, Int32)");
        }
    }
}