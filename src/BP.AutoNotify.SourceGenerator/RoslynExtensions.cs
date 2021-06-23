using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator
{
    public static class RoslynExtensions
    {
        /// <summary>
        /// Searches the syntax tree to verify if the requested attribute is linked to field declaration.
        /// </summary>
        /// <param name="fieldSyntax">Textual information about the field to inspect</param>
        /// <param name="attributeType">The type reflection information, as returned by <code>typeof(X)</code>, for the attribute to check</param>
        /// <returns>WARN; This is NOT a 100% guaranteed hit! TRUE if the attribute name is found within the syntax tree.</returns>
        public static bool HasPotentialAttribute(this FieldDeclarationSyntax fieldSyntax, Type attributeType) =>
            fieldSyntax.AttributeLists.Count > 0 &&
            fieldSyntax.AttributeLists
                .SelectMany(attrList => attrList.Attributes)
                .Select(attr => attr.Name)
                .OfType<IdentifierNameSyntax>()
                // WARN; Using string.StartsWith because an attribute reference can have multiple identifier versions!
                // - [AttributeName]
                // - [AttributeName]Attribute
                .Any(attrNameSyntax => attributeType.Name.StartsWith(attrNameSyntax.Identifier.Text, StringComparison.Ordinal));

        /// <summary>
        /// Verifies by the semantic model if the requested attribute is linked to the field symbol.
        /// </summary>
        /// <param name="fieldSymbol">Compilation information about the field to inspect</param>
        /// <param name="attributeType">The type reflection information, as returned by <code>typeof(X)</code>, for the attribute to check</param>
        /// <returns>TRUE if the attribute is linked to the symbol.</returns>
        //public static bool HasAttribute(this IFieldSymbol fieldSymbol, Type attributeType) =>
        //    fieldSymbol.GetAttributes()
        //        .Any(attr => string.Equals(attr.AttributeClass?.ToDisplayString(), attributeType.FullName, StringComparison.Ordinal));

        public static bool HasAttribute(this INamedTypeSymbol namedSymbol, INamedTypeSymbol attribute) =>
            namedSymbol.GetAttributes()
                .Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attribute));

        /// <summary>
        /// Combines all class modifying tokens into a string.
        /// 
        /// eg; <code>public partial class X</code> returns <code>public partial</code>
        /// </summary>
        /// <param name="classSyntax">Textual information about the class to inspect</param>
        /// <returns>The modifier syntax of the provided class declaration.</returns>
        public static string GetModifiersString(this ClassDeclarationSyntax classSyntax) =>
            classSyntax.Modifiers.ToFullString().Trim();

        public static string GetClassNameString(this ClassDeclarationSyntax classSyntax) =>
            classSyntax.Identifier.Text;

        /// <summary>
        /// Combines all class base types into a string.
        /// 
        /// eg; <code>class X: Object</code> returns <code>: Object</code>
        /// </summary>
        /// <param name="classSyntax">Textual information about the class to inspect</param>
        /// <returns>The base list syntax of the provided class declaration, or an empty string if the list is empty!</returns>
        public static string GetClassBaseSyntax(this ClassDeclarationSyntax classSyntax) =>
            classSyntax.BaseList?.ToFullString() ?? string.Empty;
    }
}
