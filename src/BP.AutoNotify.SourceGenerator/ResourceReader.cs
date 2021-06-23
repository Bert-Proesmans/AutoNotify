using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BP.AutoNotify.SourceGenerator
{
    public static class ResourceReader
    {
        /// <summary>
        /// Returns the content of an embedded resource.
        /// </summary>
        /// <remarks>
        /// It's important to provide a pointer to the correct assembly, because the entrypoint assembly doesn't always have
        /// the mentioned resource!
        /// </remarks>
        /// <param name="name">Name of the resource</param>
        /// <param name="assemblyPointer">Type from the assembly that should be searched.</param>
        /// <returns>String encoded value of the by name requested resource.</returns>
        public static string GetResource(string name, Type? assemblyPointer = default)
        {
            var assembly = assemblyPointer?.Assembly ?? Assembly.GetExecutingAssembly();
            // NOTE; All path slashes are replaced by dots.
            var resources = assembly.GetManifestResourceNames().Where(resName => resName.EndsWith(name));
            var resourceName = resources.Single();
            return ReadEmbeddedResource(resourceName, assembly);
        }

        public static string ReadEmbeddedResource(string resourceName, Assembly assembly)
        {
            using var resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null) return string.Empty;
            using var streamReader = new StreamReader(resourceStream);
            return streamReader.ReadToEnd();
        }
    }
}
