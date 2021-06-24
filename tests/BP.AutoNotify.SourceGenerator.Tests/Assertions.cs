using FluentAssertions;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator.Tests
{
    public static class Assertions
    {
        public static GeneratorResult VerifyCompilesSuccesfully(this GeneratorResult result)
        {
            result.RunResult.Results.Select(x => x.Exception).Where(x => x != null).Should().BeNullOrEmpty();
            return result;
        }

        public static GeneratorResult VerifySyntaxTrees(this GeneratorResult result, params string[] sources)
        {
            result.GetGeneratedSources()
                .Select(source => source.SyntaxTree)
                .Select(syntax => syntax.ToString())
                .Should()
                .BeEquivalentTo(sources);

            return result;
        }
    }
}
