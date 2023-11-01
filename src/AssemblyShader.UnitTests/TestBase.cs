// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Build.Utilities.ProjectCreation;
using System;
using System.IO;

namespace AssemblyShader.UnitTests
{
    public abstract class TestBase : MSBuildTestBase, IDisposable
    {
        private static readonly DirectoryInfo _testAssemblyDirectory = new DirectoryInfo(Path.GetDirectoryName(typeof(TestBase).Assembly.Location)!);

        private readonly DirectoryInfo _testDirectory;

        public TestBase()
        {
            _testDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N")));
        }

        public string TestAssemblyDirectory => _testAssemblyDirectory!.FullName;

        public string TestDirectory => _testDirectory.FullName;

        public void Dispose()
        {
            _testDirectory.Delete(recursive: true);
        }
    }
}