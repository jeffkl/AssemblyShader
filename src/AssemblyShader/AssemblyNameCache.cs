// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace AssemblyShader
{
    /// <summary>
    /// Represents a cache of assembly names by file path. If the assembly name has already been read for a particular assembly path, the cached value is returned. If an assembly file changes on disk, the assembly name is not reloaded.
    /// </summary>
    internal static class AssemblyNameCache
    {
        private static readonly ConcurrentDictionary<string, Lazy<AssemblyName>> Cache = new ConcurrentDictionary<string, Lazy<AssemblyName>>(StringComparer.OrdinalIgnoreCase);

        public static AssemblyName GetAssemblyName(string path) => Cache.GetOrAdd(path, new Lazy<AssemblyName>(() => AssemblyName.GetAssemblyName(path))).Value;
    }
}