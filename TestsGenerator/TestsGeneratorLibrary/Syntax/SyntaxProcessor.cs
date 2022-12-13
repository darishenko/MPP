using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Syntax 
{
    public class SyntaxProcessor
    {
        private List<ClassDeclarationSyntax> classes;
        private SyntaxNode root;
        private SyntaxTree tree;

        public SyntaxProcessResult Process(string sourceCode)
        {
            tree = CSharpSyntaxTree.ParseText(sourceCode);
            root = tree.GetRoot();
            var processResult = new SyntaxProcessResult(GetClasses());
            return processResult;
        }

        private List<ClassInformation> GetClasses()
        {
            classes = new List<ClassDeclarationSyntax>(root.DescendantNodes().OfType<ClassDeclarationSyntax>());
            var result = new List<ClassInformation>();
            foreach (var clazz in classes)
            {
                var className = clazz.Identifier.ToString();
                var classParent = (NamespaceDeclarationSyntax) clazz.Parent;
                Debug.Assert(classParent != null, nameof(classParent) + " != null");
                
                var nameSpace = classParent.Name.ToString();
                var methods = GetMethods(clazz);
                result.Add(new ClassInformation(className, nameSpace, methods));
            }
            
            return result;
        }

        private List<string> GetMethods(ClassDeclarationSyntax clazz)
        {
            return new List<string>(
                clazz.DescendantNodes().OfType<MethodDeclarationSyntax>().Where(method =>
                        method.Modifiers.Any(modifer => modifer.ToString() == "public"))
                    .Select(element => element.Identifier.ToString()));
        }
    }
}