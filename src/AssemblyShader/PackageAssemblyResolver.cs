// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AssemblyShader
{
    internal sealed class PackageAssemblyResolver : IPackageAssemblyResolver
    {
        public IEnumerable<PackageAssembly>? GetNearest(PackageIdentity packageIdentity, string nuGetPackageRoot, string targetFramework, string[] fallbackTargetFrameworks)
        {
            string path = Path.Combine(nuGetPackageRoot, packageIdentity.Id.ToLower(), packageIdentity.Version.ToLower(), "lib");

            if (!Directory.Exists(path))
            {
                return null;
            }

            IEnumerable<string> targetFrameworksToCheck = new List<string> { targetFramework }.Concat(fallbackTargetFrameworks == null ? Array.Empty<string>() : fallbackTargetFrameworks);

            List<DirectoryInfo> compatibleTargetFrameworks = Directory.EnumerateDirectories(path).Select(i => new DirectoryInfo(i)).ToList();

            DirectoryInfo? directory = null;

            foreach (string targetFrameworkToCheck in targetFrameworksToCheck)
            {
                directory = NuGetFrameworkUtility.GetNearest(
                    compatibleTargetFrameworks,
                    NuGetFramework.ParseFolder(targetFrameworkToCheck),
                    (directoryInfo) => NuGetFramework.ParseFolder(directoryInfo.Name));

                if (directory != null)
                {
                    break;
                }
            }

            if (directory is null)
            {
                return null;
            }

            return directory.EnumerateFiles("*.dll", SearchOption.AllDirectories).Select(i => GetAssemblyName(directory, i)).Where(i => i.HasValue).Select(i => i!.Value);

            PackageAssembly? GetAssemblyName(DirectoryInfo rootDirectory, FileInfo file)
            {
                AssemblyName assemblyName;

                try
                {
                    assemblyName = AssemblyNameCache.GetAssemblyName(file.FullName);
                }
                catch (Exception)
                {
                    return null;
                }

                string subdirectory = string.Equals(file.DirectoryName, rootDirectory.FullName, StringComparison.OrdinalIgnoreCase)
                    ? string.Empty
                    : file.DirectoryName!.Substring(rootDirectory.FullName.Length + 1);

                return new PackageAssembly(file.FullName, subdirectory, assemblyName);
            }
        }
    }
}