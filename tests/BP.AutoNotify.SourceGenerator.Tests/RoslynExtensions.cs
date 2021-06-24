using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator.Tests
{
    public static class RoslynExtensions
    {
        public readonly static CSharpParseOptions PARSE_OPTIONS = new(LanguageVersion.CSharp9);

        public static Compilation CreateCompilation(string testName, params string[] sources)
        {
            var syntaxTrees = sources.Select(text =>
                CSharpSyntaxTree.ParseText(text, PARSE_OPTIONS));

            // WARN; This creates a list of references for CURRENTLY LOADED libraries.
            // Do we want to inspect our binary to load all REFERENCED libraries?
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(lib => lib.IsDynamic == false)
                .Select(lib => MetadataReference.CreateFromFile(lib.Location))
                .ToList();

            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            return CSharpCompilation.Create(testName, syntaxTrees, references, compilationOptions);
        }
    }
}
