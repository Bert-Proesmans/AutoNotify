using BP.AutoNotify.SourceGenerator.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BP.AutoNotify.SourceGenerator
{
    [Generator]
    public class AutoNotifyGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AutoNotifySyntaxVisitor());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not AutoNotifySyntaxVisitor receiver) return;
            using var generatorContext = SourceGeneratorContext<AutoNotifyGenerator>.Create(context);

            var fieldSyntaxList = receiver.Fields;
            var classDescriptors = CollectSyntax(fieldSyntaxList, generatorContext);
            // TODO; Verify descriptor duplicates
            var generatorModels = ProcessSyntax(classDescriptors, generatorContext);

            try
            {
                var notifyTemplate = ResourceReader.GetResource("AutoNotify.scriban");

                foreach (var modelData in generatorModels)
                {
                    var generated = TemplateGenerator.Execute(notifyTemplate, modelData);
                    var filenameHint = string.Join("_", modelData.Classes.Select(x => x.Name));
                    // TODO; Proper filename generation!
                    context.AddSource($"{filenameHint}_generated.cs", generated);
                }
            }
            catch { Debugger.Break(); }
        }

        private IEnumerable<CompilationModel> ProcessSyntax(IEnumerable<ClassDescriptor> classes, SourceGeneratorContext<AutoNotifyGenerator> context)
        {
            var iNotifySymbol = context.GeneratorExecutionContext.Compilation.GetTypeByMetadataName(typeof(INotifyPropertyChanged).FullName) ?? throw new Exception($"Invariant breakdown!");

            var classSymbols = classes
                .Select(x => context
                    .GeneratorExecutionContext
                    .Compilation
                    .GetSemanticModel(x.SyntaxNode?.SyntaxTree)
                    .GetDeclaredSymbol(x.SyntaxNode))
                .ToList();

            var hasINotify = classSymbols.ToDictionary(symbol => symbol, symbol => symbol.HasAttribute(iNotifySymbol), SymbolEqualityComparer.Default);
            var hasSetAndNotify = classSymbols.ToDictionary(symbol => symbol, _ => false, SymbolEqualityComparer.Default);
            var hasUpdateAndNotify = classSymbols.ToDictionary(symbol => symbol, _ => false, SymbolEqualityComparer.Default);

            var compiledClassess = classes.Zip(classSymbols, (Descriptor, Symbol) => new { Descriptor, Symbol }).ToList();
            var logicalCompilationGrouping = compiledClassess
                .GroupBy(x => x.Descriptor.CompilationUnit)
                .ToDictionary(group => group.Key, group => group.ToList());
            var logicalSymbolGrouping = compiledClassess
                .GroupBy(x => x.Symbol, SymbolEqualityComparer.Default)
                .ToDictionary(group => group.Key, group => group.ToList(), SymbolEqualityComparer.Default);

            foreach (var unitGrouping in logicalCompilationGrouping)
            {
                var unit = unitGrouping.Key;
                var unitModel = new CompilationModel()
                {
                    Generator = "BP.AutoNotify.SourceGenerator",
                    GeneratorVersion = "1.0.0.0",
                    Classes = new List<ClassModel>(),
                };

                foreach (var classData in unitGrouping.Value)
                {
                    var classSymbol = classData.Symbol ?? throw new Exception($"Symbol couldn't be retrieved!");
                    var descriptor = classData.Descriptor;
                    var amountOfPartialImplementations = logicalSymbolGrouping[classSymbol].Count;
                    var requiresINotify = classData.Descriptor.Fields.Count > 0;

                    var containers = descriptor.ParentContainers
                        .Select(descriptor =>
                            ClassModelBuilder.Simple()
                            .WithName(descriptor.GetClassNameString())
                            .WithBase(string.Empty)
                            .WithModifiers(descriptor.GetModifiersString())
                            .WithNamespace(null)
                            .WithContainers(null)
                            .WithEvents(null)
                            .WithProperties(null)
                            .Build())
                        .ToList();
                    var properties = descriptor.Fields
                        .Select(field => new PropertyModel(
                            field.Declaration.Type.ToString(),
                            field.Declaration.Variables.ToString(),
                            field.Declaration.Variables.ToString(),
                            false))
                        .ToList();

                    var requiresSetAndNotify = true;
                    var requiresUpdateAndNotify = false;

                    var events = new List<EventModel>();
                    var classBaseList = descriptor.SyntaxNode.BaseList;
                    if (requiresINotify && hasINotify[classSymbol] == false)
                    {
                        events.Add(new EventModel(typeof(PropertyChangedEventHandler).FullName, nameof(INotifyPropertyChanged.PropertyChanged)));
                        var inotifyName = SyntaxFactory.ParseTypeName(typeof(INotifyPropertyChanged).FullName);
                        var inotifySyntax = SyntaxFactory.SimpleBaseType(inotifyName);
                        classBaseList = descriptor.SyntaxNode.BaseList?.AddTypes(inotifySyntax) ?? SyntaxFactory.BaseList().AddTypes(inotifySyntax);
                    }

                    var builderSetup = ClassModelBuilder.Simple()
                        .WithNamespace(classSymbol.ContainingNamespace.ToString())
                        .WithContainers(containers)
                        .WithModifiers(descriptor.SyntaxNode.GetModifiersString())
                        .WithName(descriptor.SyntaxNode.GetClassNameString())
                        .WithBase(classBaseList.ToString())
                        .WithEvents(events)
                        .WithProperties(properties);

                    // WARN; Allow for a flag true/false switch ..
                    ClassModel classModel;
                    switch (requiresSetAndNotify && hasSetAndNotify[classSymbol] == false,
                        requiresUpdateAndNotify && hasUpdateAndNotify[classSymbol] == false)
                    {
                        case (true, true):
                            {
                                classModel = builderSetup.WithSetAndNotify().WithUpdateAndNotify().Build();
                            }
                            break;
                        case (false, true):
                            {
                                classModel = builderSetup.WithUpdateAndNotify().Build();
                            }
                            break;
                        case (true, false):
                            {
                                classModel = builderSetup.WithSetAndNotify().Build();
                            }
                            break;
                        default:
                            {
                                classModel = builderSetup.Build();
                            }
                            break;
                    }
                    unitModel.Classes.Add(classModel);
                }

                yield return unitModel;
            }
        }

        private IEnumerable<ClassDescriptor> CollectSyntax(IEnumerable<FieldDeclarationSyntax> fields, SourceGeneratorContext<AutoNotifyGenerator> context)
        {
            var logicalClassGrouping = fields.GroupBy(x => x.FirstAncestorOrSelf<ClassDeclarationSyntax>());
            foreach (var fieldGroup in logicalClassGrouping)
            {
                var classSyntax = fieldGroup.Key ?? throw new Exception($"Invariant breakdown!");
                var compilationSyntax = classSyntax.FirstAncestorOrSelf<CompilationUnitSyntax>();
                var parentDeclarations = classSyntax.Ancestors().OfType<ClassDeclarationSyntax>().Reverse().ToList();
                var classFieldSyntax = fieldGroup.ToList();

                yield return new ClassDescriptor()
                {
                    CompilationUnit = compilationSyntax,
                    ParentContainers = parentDeclarations,
                    SyntaxNode = classSyntax,
                    Fields = classFieldSyntax
                };
            }
        }
    }
}
