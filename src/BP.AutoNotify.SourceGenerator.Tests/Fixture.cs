using Microsoft.CodeAnalysis;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BP.AutoNotify.SourceGenerator.Tests
{
    public static class Fixture
    {
        public const string TEST_DATA_FOLDER = "resources";

        public static async Task<Compilation> GetInputCompilation(string test) =>
            RoslynExtensions.CreateCompilation(test, await GetTestInputs(test));

        public static Task<string[]> GetTestInputs(string test) => 
            new Matcher()
            .AddInclude($"**/{test}*.input.cs")
            .Execute(new DirectoryInfoWrapper(new DirectoryInfo(TEST_DATA_FOLDER)))
            .Files
            .Select(x => GetSourceFromFile(x.Path))
            .Flatten();

        public static Task<string[]> GetTestVerifications(string test) =>
            new Matcher()
            .AddInclude($"**/{test}*.validation.cs")
            .Execute(new DirectoryInfoWrapper(new DirectoryInfo(TEST_DATA_FOLDER)))
            .Files
            .Select(x => GetSourceFromFile(x.Path))
            .Flatten();

        public static Task<string> GetSourceFromFile(string test) => File.ReadAllTextAsync(Path.Combine("Resources", test));

        private static async Task<T[]> Flatten<T>(this IEnumerable<Task<T>> tasks) => (await Task.WhenAll(tasks));
    }
}
