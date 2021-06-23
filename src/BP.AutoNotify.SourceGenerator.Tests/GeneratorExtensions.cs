using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace BP.AutoNotify.SourceGenerator.Tests
{
    public static class GeneratorExtensions
    {
        public static GeneratorResult Run(this ISourceGenerator generator, Compilation userCode)
        {
            GeneratorDriver driver = CSharpGeneratorDriver.Create(
                generators: ImmutableArray.Create(generator),
                additionalTexts: ImmutableArray<AdditionalText>.Empty,
                parseOptions: RoslynExtensions.PARSE_OPTIONS);
            // NOTE; GeneratorDriver is IMMUTABLE, so record the returned objects!
            driver = driver.RunGeneratorsAndUpdateCompilation(userCode, out var outputCompilation, out var diagnostics);
            var runResult = driver.GetRunResult();

            return new GeneratorResult(diagnostics, runResult, userCode, outputCompilation);
        }
    }
}
