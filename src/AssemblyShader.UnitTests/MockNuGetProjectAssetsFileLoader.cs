// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System.Collections.Generic;

namespace AssemblyShader.UnitTests
{
    internal class MockNuGetProjectAssetsFileLoader : Dictionary<string, Dictionary<PackageIdentity, HashSet<PackageIdentity>>>, INuGetProjectAssetsFileLoader
    {
        public NuGetProjectAssetsFile Load(string projectDirectory, string projectAssetsFile)
        {
            NuGetProjectAssetsFile assetsFile = new NuGetProjectAssetsFile();

            foreach (var item in this)
            {
                foreach (var package in item.Value)
                {
                    assetsFile[item.Key].Packages[package.Key] = package.Value;
                }
            }

            return assetsFile;
        }
    }
}