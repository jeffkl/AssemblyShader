// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader
{
    /// <summary>
    /// Represents an interface for a class that reads assembly references.
    /// </summary>
    internal interface IAssemblyInformationReader
    {
        /// <summary>
        /// Gets assembly information for the specified assembly paths.
        /// </summary>
        /// <param name="assemblyPaths">An <see cref="IEnumerable{T}" /> containing paths to assemblies.</param>
        /// <returns>An <see cref="AssemblyInformation" /> object containing information about the assemblies.</returns>
        AssemblyInformation GetAssemblyInformation(IEnumerable<string> assemblyPaths);
    }
}