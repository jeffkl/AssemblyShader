// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace AssemblyShader
{
    internal sealed class NuGetProjectAssetsFile : Dictionary<string, NuGetProjectAssetsFileSection>
    {
        public NuGetProjectAssetsFile()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public Dictionary<string, PackageIdentity> ProjectReferences { get; } = new Dictionary<string, PackageIdentity>(StringComparer.OrdinalIgnoreCase);
    }
}