using System.Threading.Tasks;
using Xunit;

namespace BP.AutoNotify.SourceGenerator.Tests
{
    public class AutoNotifyGeneratorTests
    {
        [Fact]
        public async Task Test_001()
        {
            new AutoNotifyGenerator()
                // NOTE; Requires the existence of Test_001.input.cs and Test_001.validation.cs files
                .Run(await Fixture.GetInputCompilation(nameof(Test_001)))
                .VerifyCompilesSuccesfully()
                .VerifySyntaxTrees(await Fixture.GetTestVerifications(nameof(Test_001)));
        }
    }
}
