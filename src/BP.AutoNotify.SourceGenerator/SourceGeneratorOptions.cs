using Microsoft.CodeAnalysis;

namespace BP.AutoNotify.SourceGenerator
{
    public class SourceGeneratorOptions<TGenerator>
        where TGenerator : ISourceGenerator
    {
        public SourceGeneratorOptions(GeneratorExecutionContext context)
        {
            // TODO; Explain MSBuild property use
            if (TryReadGlobalOption(context, "SourceGenerator_EnableDebug", out var enableDebug) &&
                bool.TryParse(enableDebug, out var enableDebugValue))
            {
                EnableDebugging = enableDebugValue;
            }

            // TODO; Explain MSBuild property use
            if (TryReadGlobalOption(context, $"SourceGenerator_EnableDebug_{typeof(TGenerator).Name}", out var enableDebugThisGenerator) &&
                bool.TryParse(enableDebugThisGenerator, out var enableDebugThisGeneratorValue))
            {
                EnableDebugging = enableDebugThisGeneratorValue;
            }
        }

        public bool EnableDebugging { get; set; }

        public bool TryReadGlobalOption(GeneratorExecutionContext context, string property, out string? value) =>
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.{property}", out value);
    }
}
