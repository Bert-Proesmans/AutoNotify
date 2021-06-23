using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator
{
    public class AutoNotifySyntaxVisitor : ISyntaxContextReceiver
    {
        private readonly List<FieldDeclarationSyntax> _fields = new();
        public ImmutableArray<FieldDeclarationSyntax> Fields => _fields.ToImmutableArray();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is FieldDeclarationSyntax fieldDeclarationSyntax &&
                fieldDeclarationSyntax.HasPotentialAttribute(typeof(Abstractions.AutoNotifyAttribute)))
            {
                _fields.Add(fieldDeclarationSyntax);
            }
        }
    }
}
