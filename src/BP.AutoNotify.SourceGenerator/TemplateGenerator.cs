using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Scriban;
using System;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator
{
    public static class TemplateGenerator
    {
        public static string Execute(string templateString, object model)
        {
            var template = Template.Parse(templateString);
            if(template.HasErrors)
            {
                // TODO; Complete error handling
                throw new Exception($"Template parse error: {template.Messages.First().Message}");
            }

            var render = template.Render(model, member => member.Name);
            var normalizedRender = SyntaxFactory.ParseCompilationUnit(render)
                .NormalizeWhitespace()
                .GetText()
                .ToString();
            return normalizedRender;
        }
    }
}
