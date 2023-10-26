// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader
{
    /// <summary>
    /// Represents an interface for a class that resolves assemblies in a NuGet package.
    /// </summary>
    internal interface IPackageAssemblyResolver
    {
        /// <summary>
        /// Gets the assembly paths for the specified package based on the supported target frameworks.
        /// </summary>
        /// <param name="packageIdentity">The <see cref="PackageIdentity" /> of the package.</param>
        /// <param name="nuGetPackageRoot">The root directory containing NuGet packages.</param>
        /// <param name="targetFramework">The target framework of the current project.</param>
        /// <param name="fallbackTargetFrameworks">An array of fallback target frameworks of the current project.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> containing the resolved assemblies from the package for the target framework.</returns>
        IEnumerable<PackageAssembly> GetNearest(PackageIdentity packageIdentity, string nuGetPackageRoot, string targetFramework, string[] fallbackTargetFrameworks);
    }
}