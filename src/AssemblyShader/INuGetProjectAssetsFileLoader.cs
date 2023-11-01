// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader
{
    /// <summary>
    /// Represents an interface for a class that loads NuGet project assets files.
    /// </summary>
    internal interface INuGetProjectAssetsFileLoader
    {
        /// <summary>
        /// Loads the specified NuGet assets file.
        /// </summary>
        /// <param name="projectDirectory">The full path to the project's directory.</param>
        /// <param name="projectAssetsFile">The full path to the NuGet assets file to load.</param>
        /// <returns>An <see cref="Dictionary{TKey, TValue}" /> containing target frameworks and a list of packages with their dependencies.</returns>
        NuGetProjectAssetsFile? Load(string projectDirectory, string projectAssetsFile);
    }
}