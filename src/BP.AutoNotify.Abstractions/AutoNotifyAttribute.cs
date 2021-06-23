using System;

namespace BP.AutoNotify.Abstractions
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class AutoNotifyAttribute : Attribute
    {
        public AutoNotifyAttribute()
        {
        }

        public string PropertyName { get; set; }
    }
}
