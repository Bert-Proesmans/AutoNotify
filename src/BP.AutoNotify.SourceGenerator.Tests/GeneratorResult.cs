using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator.Tests
{
    public class GeneratorResult
    {
        public GeneratorResult(ImmutableArray<Diagnostic> diagnostics, GeneratorDriverRunResult runResult, Compilation inputCompilation, Compilation outputCompilation)
        {
            Diagnostics = diagnostics;
            RunResult = runResult ?? throw new ArgumentNullException(nameof(runResult));
            InputCompilation = inputCompilation ?? throw new ArgumentNullException(nameof(inputCompilation));
            OutputCompilation = outputCompilation ?? throw new ArgumentNullException(nameof(outputCompilation));
        }

        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public GeneratorDriverRunResult RunResult { get; }
        public Compilation InputCompilation { get; }
        public Compilation OutputCompilation { get; }

        /// <summary>
        /// Returns the syntax trees found after running the code generator that weren't exactly provided as generator input.
        /// </summary>
        /// <returns>New or updated syntax trees after code generation ran to completion</returns>
        public IEnumerable<GeneratedSourceResult> GetGeneratedSources()
        {
            return RunResult.Results.SelectMany(x => x.GeneratedSources);
            //var inputRoots = InputCompilation.SyntaxTrees.Select(tree => tree.GetRoot()).ToHashSet();
            //foreach (var tree in OutputCompilation.SyntaxTrees)
            //{
            //    var rootNode = tree.GetRoot();
            //    if (inputRoots.Contains(rootNode)) continue;
            //    yield return tree;
            //}
        }
    }
}
