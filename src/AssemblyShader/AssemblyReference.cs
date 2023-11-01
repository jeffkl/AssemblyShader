// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.IO;
using System.Reflection;

namespace AssemblyShader
{
    internal readonly record struct AssemblyReference
    {
        public AssemblyReference(string fullPath, AssemblyName name)
        {
            FullPath = fullPath;
            Name = name;
        }

        public AssemblyReference(string fullPath)
            : this(fullPath, AssemblyNameCache.GetAssemblyName(fullPath))
        {
        }

        public AssemblyReference(FileInfo fullPath, string assemblyName)
            : this(fullPath.FullName, new AssemblyName(assemblyName))
        {
        }

        public readonly string FullPath { get; }

        public readonly AssemblyName Name { get; }
    }
}