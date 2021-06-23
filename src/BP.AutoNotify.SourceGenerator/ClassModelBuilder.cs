using System;
using System.Collections.Generic;

namespace BP.AutoNotify.SourceGenerator
{
    public enum Eps { }

    public static class ClassModelBuilder
    {
        public static Builders.IClassModelBuilder<Eps, Eps, Eps, Eps, Eps, Eps, Eps, Eps, Eps> Simple() => Builders.Interner.Simple();
    }

    namespace Builders
    {
        public interface IClassModelBuilder<
            TNamespacePath,
            TContainers,
            TModifiers,
            TName,
            TBase,
            TProperties,
            TEvents,
            TSetAndNotify,
            TUpdateAndNotify>
        {
        }

        public static class Interner
        {
            // REMARK; Take note of ? fields to initialize null everywhere
            // REMARK; Take note of the Func<X>? fields to allow for on-demand or remote value generation
            // REMARK; Take note of the builder functions WithSetAndNotify and WithUpdateAndNotify which do NOT take any additional parameter
            // since they act as flag fields.
            private sealed class ClassModelBuilder<
            TNamespacePath,
            TContainers,
            TModifiers,
            TName,
            TBase,
            TProperties,
            TEvents,
            TSetAndNotify,
            TUpdateAndNotify> :
            IClassModelBuilder<
                TNamespacePath,
                TContainers,
                TModifiers,
                TName,
                TBase,
                TProperties,
                TEvents,
                TSetAndNotify,
                TUpdateAndNotify>
            {
                public ClassModelBuilder() { }

                public Func<TNamespacePath>? _namespacePath;
                public Func<TContainers>? _containers;
                public Func<TModifiers>? _modifiers;
                public Func<TName>? _name;
                public Func<TBase>? _base;
                public Func<TProperties>? _properties;
                public Func<TEvents>? _events;
                public Func<TSetAndNotify>? _setAndNotify;
                public Func<TUpdateAndNotify>? _updateAndNotify;
            }

            public static IClassModelBuilder<Eps, Eps, Eps, Eps, Eps, Eps, Eps, Eps, Eps> Simple() => new ClassModelBuilder<Eps, Eps, Eps, Eps, Eps, Eps, Eps, Eps, Eps>();

            public static IClassModelBuilder<string, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> WithNamespace<TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<Eps, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> _b, string namespacePath)
            {
                var builder = (ClassModelBuilder<Eps, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<string, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = () => namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, List<ClassModel>, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> WithContainers<TNamespacePath, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, Eps, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> _b, List<ClassModel> containers)
            {
                var builder = (ClassModelBuilder<TNamespacePath, Eps, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, List<ClassModel>, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = () => containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, string, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> WithModifiers<TNamespacePath, TContainers, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, TContainers, Eps, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> _b, string modifiers)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, Eps, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, string, TName, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = () => modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, TModifiers, string, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> WithName<TNamespacePath, TContainers, TModifiers, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, TContainers, TModifiers, Eps, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> _b, string name)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, TModifiers, Eps, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, TModifiers, string, TBase, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = () => name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, string, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> WithBase<TNamespacePath, TContainers, TName, TModifiers, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, Eps, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> _b, string @base)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, Eps, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, string, TProperties, TEvents, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = () => @base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, List<PropertyModel>, TEvents, TSetAndNotify, TUpdateAndNotify> WithProperties<TNamespacePath, TContainers, TModifiers, TBase, TName, TEvents, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, Eps, TEvents, TSetAndNotify, TUpdateAndNotify> _b, List<PropertyModel> properties)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, Eps, TEvents, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, List<PropertyModel>, TEvents, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = () => properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, List<EventModel>, TSetAndNotify, TUpdateAndNotify> WithEvents<TNamespacePath, TContainers, TName, TModifiers, TProperties, TBase, TSetAndNotify, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, Eps, TSetAndNotify, TUpdateAndNotify> _b, List<EventModel> events)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, Eps, TSetAndNotify, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, List<EventModel>, TSetAndNotify, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = () => events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, bool, TUpdateAndNotify> WithSetAndNotify<TNamespacePath, TContainers, TModifiers, TBase, TName, TEvents, TProperties, TUpdateAndNotify>(
                this IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, Eps, TUpdateAndNotify> _b)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, Eps, TUpdateAndNotify>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, bool, TUpdateAndNotify> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = () => true;
                b._updateAndNotify = builder._updateAndNotify;
                return b;
            }

            public static IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, bool> WithUpdateAndNotify<TNamespacePath, TContainers, TName, TModifiers, TProperties, TBase, TSetAndNotify, TEvents>(
                this IClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, Eps> _b)
            {
                var builder = (ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, Eps>)_b;
                ClassModelBuilder<TNamespacePath, TContainers, TModifiers, TName, TBase, TProperties, TEvents, TSetAndNotify, bool> b = new();
                b._namespacePath = builder._namespacePath;
                b._containers = builder._containers;
                b._modifiers = builder._modifiers;
                b._name = builder._name;
                b._base = builder._base;
                b._properties = builder._properties;
                b._events = builder._events;
                b._setAndNotify = builder._setAndNotify;
                b._updateAndNotify = () => true;
                return b;
            }

            public static ClassModel Build(this IClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, bool, bool> _b)
            {
                var builder = (ClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, bool, bool>)_b;
                return new(builder._namespacePath!(), builder._containers!(), builder._modifiers!(), builder._name!(), builder._base!(), builder._properties!(), builder._events!(), builder._setAndNotify!(), builder._updateAndNotify!());
            }

            public static ClassModel Build(this IClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, Eps, bool> _b)
            {
                var builder = (ClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, Eps, bool>)_b;
                return new(builder._namespacePath!(), builder._containers!(), builder._modifiers!(), builder._name!(), builder._base!(), builder._properties!(), builder._events!(), false, builder._updateAndNotify!());
            }

            public static ClassModel Build(this IClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, bool, Eps> _b)
            {
                var builder = (ClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, bool, Eps>)_b;
                return new(builder._namespacePath!(), builder._containers!(), builder._modifiers!(), builder._name!(), builder._base!(), builder._properties!(), builder._events!(), builder._setAndNotify!(), false);
            }

            public static ClassModel Build(this IClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, Eps, Eps> _b)
            {
                var builder = (ClassModelBuilder<string, List<ClassModel>, string, string, string, List<PropertyModel>, List<EventModel>, Eps, Eps>)_b;
                return new(builder._namespacePath!(), builder._containers!(), builder._modifiers!(), builder._name!(), builder._base!(), builder._properties!(), builder._events!(), false, false);
            }
        }
    }
}
