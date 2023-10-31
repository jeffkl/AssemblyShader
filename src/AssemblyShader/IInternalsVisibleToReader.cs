// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Runtime.CompilerServices;

namespace AssemblyShader
{
    /// <summary>
    /// Represents an interface for a class that gets the value for an <see cref="InternalsVisibleToAttribute" /> for assemblies.
    /// </summary>
    internal interface IInternalsVisibleToReader
    {
        /// <summary>
        /// Gets the value for an <see cref="InternalsVisibleToAttribute" /> for the specified assembly path.
        /// </summary>
        /// <param name="path">The full path to the assembly to get a value for an <see cref="InternalsVisibleToAttribute" />.</param>
        /// <returns>A value for an <see cref="InternalsVisibleToAttribute" /> for the specified assembly.</returns>
        public string Read(string path);
    }
}