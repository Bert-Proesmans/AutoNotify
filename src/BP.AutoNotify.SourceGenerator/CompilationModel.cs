using System;
using System.Collections.Generic;
using System.Text;

namespace BP.AutoNotify.SourceGenerator
{
    public class CompilationModel
    {
        public List<ClassModel> Classes { get; set; }
        public string Generator { get; set; }
        public string GeneratorVersion { get; set; }
    }

    public class ClassModel
    {
        public ClassModel(string namespacePath, List<ClassModel> containers, string modifiers, string name, string @base, List<PropertyModel> properties, List<EventModel> events, bool setAndNotify, bool updateAndNotify)
        {
            NamespacePath = namespacePath ?? throw new ArgumentNullException(nameof(namespacePath));
            Containers = containers ?? throw new ArgumentNullException(nameof(containers));
            Modifiers = modifiers ?? throw new ArgumentNullException(nameof(modifiers));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Base = @base ?? throw new ArgumentNullException(nameof(@base));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
            Events = events ?? throw new ArgumentNullException(nameof(events));
            SetAndNotify = setAndNotify;
            UpdateAndNotify = updateAndNotify;
        }

        public string NamespacePath { get; }
        public List<ClassModel> Containers { get; }
        public string Modifiers { get; }

        public string Name { get; }

        public string Base { get; }

        public List<PropertyModel> Properties { get; }

        public List<EventModel> Events { get; }

        public bool SetAndNotify { get; }

        public bool UpdateAndNotify { get; }
    }

    public class PropertyModel
    {
        public PropertyModel(string type, string name, string fieldPath, bool compareAndSet)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FieldPath = fieldPath ?? throw new ArgumentNullException(nameof(fieldPath));
            CompareAndSet = compareAndSet;
        }

        public string Type { get; }
        public string Name { get; }
        public string FieldPath { get; }
        public bool CompareAndSet { get; }
    }

    public class EventModel
    {
        public EventModel(string type, string name)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Type { get; }
        public string Name { get; }
    }
}
