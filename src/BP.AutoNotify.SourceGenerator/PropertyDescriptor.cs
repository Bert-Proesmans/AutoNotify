using System;
using System.Collections.Generic;
using System.Text;

namespace BP.AutoNotify.SourceGenerator
{
    public class PropertyDescriptor
    {
        public string Type { get; }
        public string Name { get; }
        public string FieldName { get; }
        public bool CompareAndSet { get; }
    }
}
