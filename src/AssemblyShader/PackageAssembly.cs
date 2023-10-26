// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Reflection;

namespace AssemblyShader
{
    internal record struct PackageAssembly
    {
        public PackageAssembly(string path, string subDirectory, AssemblyName assemblyName)
        {
            Path = path;
            Subdirectory = subDirectory;
            Name = assemblyName;
        }

        public readonly string Path { get; }

        public readonly string Subdirectory { get; }

        public readonly AssemblyName Name { get; }
    }
}