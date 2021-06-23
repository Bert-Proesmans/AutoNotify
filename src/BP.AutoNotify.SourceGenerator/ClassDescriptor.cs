using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace BP.AutoNotify.SourceGenerator
{
    public class ClassDescriptor
    {
        public INamedTypeSymbol? Symbol { get; set; }
        public CompilationUnitSyntax? CompilationUnit { get; set; }
        public List<ClassDeclarationSyntax>? ParentContainers { get; set; }
        public ClassDeclarationSyntax? SyntaxNode { get; set; }
        public List<FieldDeclarationSyntax>? Fields { get; set; }
    }
}
