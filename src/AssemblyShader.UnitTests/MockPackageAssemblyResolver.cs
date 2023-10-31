// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader.UnitTests
{
    internal sealed class MockPackageAssemblyResolver : Dictionary<PackageIdentity, List<PackageAssembly>>, IPackageAssemblyResolver
    {
        public IEnumerable<PackageAssembly> GetNearest(PackageIdentity packageIdentity, string nuGetPackageRoot, string targetFramework, string[] fallbackTargetFrameworks)
        {
            return this[packageIdentity];
        }
    }
}