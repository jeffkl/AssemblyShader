// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader
{
    internal sealed class NuGetProjectAssetsFileSection
    {
        public Dictionary<PackageIdentity, HashSet<PackageIdentity>> Packages { get; } = new Dictionary<PackageIdentity, HashSet<PackageIdentity>>();
    }
}