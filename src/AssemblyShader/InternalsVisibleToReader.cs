// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Mono.Cecil;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace AssemblyShader
{
    internal sealed class InternalsVisibleToReader : IInternalsVisibleToReader
    {
        private static readonly ConcurrentDictionary<string, Lazy<string>> Cache = new ConcurrentDictionary<string, Lazy<string>>(StringComparer.OrdinalIgnoreCase);

        public string Read(string path)
        {
            Lazy<string> result = Cache.GetOrAdd(path, new Lazy<string>(() => GetInternalsVisibleToAttributeValue(path)));

            return result.Value;
        }

        private static string GetInternalsVisibleToAttributeValue(string path)
        {
            using AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(path);

            if (assemblyDefinition.Name.HasPublicKey)
            {
                return $"{assemblyDefinition.Name.Name}, PublicKey={string.Join(string.Empty, assemblyDefinition.Name.PublicKey.Select(i => i.ToString("x2")))}";
            }

            return assemblyDefinition.Name.Name;
        }
    }
}