// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Build.Framework;
using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyShader
{
    /// <summary>
    /// Represents an assembly that will be renamed.
    /// </summary>
    [DebuggerDisplay("{AssemblyName, nq} => {ShadedAssemblyName,nq} ({ShadedPath,nq}}")]
    internal sealed class AssemblyToRename : IEquatable<AssemblyToRename>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyToRename" /> class.
        /// </summary>
        /// <param name="fullPath">The full path to the assembly.</param>
        /// <param name="assemblyName">The <see cref="AssemblyName" /> of the assembly.</param>
        /// <param name="taskItem">An optional <see cref="ITaskItem" /> to populate the <see cref="Metadata" /> dictionary with.</param>
        public AssemblyToRename(string fullPath, AssemblyName assemblyName, ITaskItem? taskItem = null)
        {
            FullPath = fullPath;
            AssemblyName = assemblyName;
            Metadata = taskItem == null ? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) : taskItem.CloneCustomMetadata();
        }

        /// <summary>
        /// Gets the <see cref="AssemblyName" /> of the assembly.
        /// </summary>
        public AssemblyName AssemblyName { get; }

        /// <summary>
        /// Gets or sets the destination subdirectory for the assembly.
        /// </summary>
        public string? DestinationSubdirectory { get; set; }

        /// <summary>
        /// Gets the full path to the assembly.
        /// </summary>
        public string FullPath { get; }

        /// <summary>
        /// Gets or sets the value of a <see cref="InternalsVisibleToAttribute" /> that would reference the assembly.
        /// </summary>
        public string? InternalsVisibleTo { get; set; }

        /// <summary>
        /// Gets an <see cref="IDictionary" /> containing the metadata for an MSBuild item.
        /// </summary>
        public IDictionary Metadata { get; }

        /// <summary>
        /// Gets or sets the <see cref="AssemblyNameDefinition" /> of the shaded assembly.
        /// </summary>
        public AssemblyNameDefinition? ShadedAssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the value of a <see cref="InternalsVisibleToAttribute" /> that would reference the assembly after it has been shaded.
        /// </summary>
        public string? ShadedInternalsVisibleTo { get; set; }

        /// <summary>
        /// Gets or sets the full path to the shaded assembly.
        /// </summary>
        public string? ShadedPath { get; set; }

        public bool Equals(AssemblyToRename? other)
        {
            return other is not null && string.Equals(AssemblyName.FullName, other.AssemblyName.FullName);
        }
    }
}