// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssemblyShader
{
    /// <summary>
    /// Represents a NuGet package's identity.
    /// </summary>
    [DebuggerDisplay("{Id,nq}/{Version,nq}")]
    internal readonly record struct PackageIdentity : IEqualityComparer<PackageIdentity>, IEquatable<PackageIdentity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageIdentity" /> class with the specified ID and version.
        /// </summary>
        /// <param name="id">The ID of the package.</param>
        /// <param name="version">The version of the package.</param>
        /// <exception cref="ArgumentNullException"><paramref name="id" /> or <paramref name="version" /> are <see langword="null" />.</exception>
        public PackageIdentity(string id, string version)
        {
            Id = id.ThrowIfNull(nameof(id)).Trim();
            Version = version.ThrowIfNull(nameof(version)).Trim();
        }

        /// <summary>
        /// Gets the ID of the package.
        /// </summary>
        public readonly string Id { get; }

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public readonly string Version { get; }

        public bool Equals(PackageIdentity x, PackageIdentity y) => string.Equals(x.Id, y.Id, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Version, y.Version, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode(PackageIdentity obj)
        {
#if NETSTANDARD
            return Id.GetHashCode() ^ Version.GetHashCode();
#else
            return HashCode.Combine(obj.Id, obj.Version);
#endif
        }

        public override string ToString()
        {
            return $"{Id}/{Version}";
        }
    }
}