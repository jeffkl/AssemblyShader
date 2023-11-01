// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace AssemblyShader
{
    /// <summary>
    /// Represents an implementation of <see cref="IEqualityComparer{T}" /> that compares <see cref="FileSystemInfo" /> objects by the value of their <see cref="FileSystemInfo.FullName" /> property.
    /// </summary>
    internal sealed class FileSystemInfoFullNameEqualityComparer : IEqualityComparer<FileSystemInfo>
    {
        private FileSystemInfoFullNameEqualityComparer()
        {
        }

        /// <summary>
        /// Gets a static singleton for the <see cref="FileSystemInfoFullNameEqualityComparer" /> class.
        /// </summary>
        public static FileSystemInfoFullNameEqualityComparer Instance { get; } = new FileSystemInfoFullNameEqualityComparer();

        /// <summary>
        /// Determines whether the specified <see cref="FileSystemInfo" /> objects are equal by comparing their <see cref="FileSystemInfo.FullName" /> property.
        /// </summary>
        /// <param name="x">The first <see cref="FileSystemInfo" /> to compare.</param>
        /// <param name="y">The second <see cref="FileSystemInfo" /> to compare.</param>
        /// <returns><see langword="true" /> if the specified <see cref="FileSystemInfo" /> objects' <see cref="FileSystemInfo.FullName" /> property are equal, otherwise <see langword="false" />.</returns>
        public bool Equals(FileSystemInfo? x, FileSystemInfo? y)
        {
            return x is not null
                && y is not null
                && string.Equals(x.FullName, y.FullName, RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns a hash code for the specified <see cref="FileSystemInfo" /> object's <see cref="FileSystemInfo.FullName" /> property.
        /// </summary>
        /// <param name="obj">The <see cref="FileSystemInfo" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified <see cref="FileSystemInfo" /> object's <see cref="FileSystemInfo.FullName" /> property..</returns>
        public int GetHashCode(FileSystemInfo obj)
        {
#if NETFRAMEWORK || NETSTANDARD
            return obj.FullName.GetHashCode();
#else
            return obj.FullName.GetHashCode(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
#endif
        }
    }
}