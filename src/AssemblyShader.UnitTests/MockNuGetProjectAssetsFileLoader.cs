// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader.UnitTests
{
    internal sealed class MockNuGetProjectAssetsFileLoader : Dictionary<string, Dictionary<PackageIdentity, HashSet<PackageIdentity>>>, INuGetProjectAssetsFileLoader
    {
        public NuGetProjectAssetsFile Load(string projectDirectory, string projectAssetsFile)
        {
            NuGetProjectAssetsFile assetsFile = new NuGetProjectAssetsFile();

            foreach (KeyValuePair<string, Dictionary<PackageIdentity, HashSet<PackageIdentity>>> item in this)
            {
                assetsFile[item.Key] = new NuGetProjectAssetsFileSection();

                foreach (KeyValuePair<PackageIdentity, HashSet<PackageIdentity>> package in item.Value)
                {
                    assetsFile[item.Key].Packages[package.Key] = package.Value;
                }
            }

            return assetsFile;
        }
    }
}