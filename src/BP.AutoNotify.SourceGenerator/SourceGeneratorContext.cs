using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;

namespace BP.AutoNotify.SourceGenerator
{
    public class SourceGeneratorContext<TGenerator> : IDisposable
        where TGenerator : ISourceGenerator
    {
        private bool disposedValue;

        private SourceGeneratorContext(GeneratorExecutionContext context)
        {
            GeneratorExecutionContext = context;
            Options = new SourceGeneratorOptions<TGenerator>(context);
        }

        public GeneratorExecutionContext GeneratorExecutionContext { get; }
        public SourceGeneratorOptions<TGenerator> Options { get; }

        public static SourceGeneratorContext<TGenerator> Create(GeneratorExecutionContext context)
        {
            var generatorContext = new SourceGeneratorContext<TGenerator>(context);
            
            if(generatorContext.Options.EnableDebugging)
            {
                if(!Debugger.IsAttached)
                {
                    Debugger.Launch();
                }
            }

            return generatorContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
