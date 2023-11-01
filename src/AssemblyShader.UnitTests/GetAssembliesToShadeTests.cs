// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using AssemblyShader.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace AssemblyShader.UnitTests
{
    public sealed class GetAssembliesToShadeTests : TestBase
    {
        [Fact]
        public void Test1()
        {
            PackageIdentity packageMicrosoftNETTestSdk = new PackageIdentity("Microsoft.NET.Test.Sdk", "17.3.0");
            PackageIdentity packageMicrosoftNETCommunication = new PackageIdentity("Microsoft.NET.Communication", "17.3.0");
            PackageIdentity packageMicrosoftNETLogging = new PackageIdentity("Microsoft.NET.Logging", "17.3.0");
            PackageIdentity packageNewtonsoftJson9 = new PackageIdentity("Newtonsoft.Json", "9.0.1");
            PackageIdentity packageNewtonsoftJson12 = new PackageIdentity("Newtonsoft.Json", "12.0.3");
            PackageIdentity packageMicrosoftJsonBson = new PackageIdentity("Microsoft.Json.Bson", "1.0.2");

            string pathNewtonsoftJson12 = Path.Combine(TestDirectory, "Newtonsoft.Json", "12.0.3", "lib", "net45", "Newtonsoft.Json.dll");
            string pathNewtonsoftJson9 = Path.Combine(TestDirectory, "Newtonsoft.Json", "9.0.1", "lib", "net45", "Newtonsoft.Json.dll");
            string pathNewtonsoftJsonBson = Path.Combine(TestDirectory, "Newtonsoft.Json.Bson", "1.0.2", "lib", "net45", "Newtonsoft.Json.Bson.dll");
            string pathMicrosoftNETTestSdk = Path.Combine(TestDirectory, packageMicrosoftNETTestSdk.Id, packageMicrosoftNETTestSdk.Version, "lib", "net45", $"{packageMicrosoftNETTestSdk.Id}.dll");
            string pathMicrosoftNETCommunication = Path.Combine(TestDirectory, packageMicrosoftNETCommunication.Id, packageMicrosoftNETCommunication.Version, "lib", "net45", $"{packageMicrosoftNETCommunication.Id}.dll");
            string pathMicrosoftNETLogging = Path.Combine(TestDirectory, packageMicrosoftNETLogging.Id, packageMicrosoftNETLogging.Version, "lib", "net45", $"{packageMicrosoftNETLogging.Id}.dll");

            AssemblyName assemblyNameNewtonsoftJson9 = new AssemblyName("Newtonsoft.Json, Version=9.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            AssemblyName assemblyNameNewtonsoftJson12 = new AssemblyName("Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            AssemblyName assemblyNameNewtonsoftJsonBson = new AssemblyName("Newtonsoft.Json.Bson, Version=1.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            AssemblyName assemblyNameMicrosoftNETTestSdk = new AssemblyName("Microsoft.NET.Test.Sdk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            AssemblyName assemblyNameMicrosoftNETCommunication = new AssemblyName("Microsoft.NET.Communication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            AssemblyName assemblyNameMicrosoftNETLogging = new AssemblyName("Microsoft.NET.Logging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");

            INuGetProjectAssetsFileLoader assetsFileLoader = new MockNuGetProjectAssetsFileLoader
            {
                [".NETFramework,Version=v4.7.2"] = new Dictionary<PackageIdentity, HashSet<PackageIdentity>>
                {
                    [packageMicrosoftNETTestSdk] = new HashSet<PackageIdentity>
                    {
                        packageNewtonsoftJson9,
                    },
                    [packageMicrosoftJsonBson] = new HashSet<PackageIdentity>
                    {
                        packageNewtonsoftJson12,
                    },
                },
            };

            ITaskItem[] packageReferences = new ITaskItem[]
            {
                new TaskItem("Microsoft.NET.Test.Sdk", new Dictionary<string, string>
                {
                    { "Version", "17.3.0" },
                    { "ShadeDependencies", "Newtonsoft.Json" },
                }),
                new TaskItem("Microsoft.Json.Bson", new Dictionary<string, string>
                {
                    { "Version", "1.0.2" },
                }),
            };

            ITaskItem[] references = new ITaskItem[]
            {
                new TaskItem(pathMicrosoftNETTestSdk, new Dictionary<string, string>
                {
                    { "FullPath", pathMicrosoftNETTestSdk },
                }),
                new TaskItem(pathNewtonsoftJson12, new Dictionary<string, string>
                {
                    { "FullPath", pathNewtonsoftJson12 },
                }),
                new TaskItem(pathNewtonsoftJsonBson, new Dictionary<string, string>
                {
                    { "FullPath", pathNewtonsoftJsonBson },
                }),
                new TaskItem(pathMicrosoftNETCommunication, new Dictionary<string, string>
                {
                    { "FullPath", pathMicrosoftNETCommunication },
                }),
                new TaskItem(pathMicrosoftNETLogging, new Dictionary<string, string>
                {
                    { "FullPath", pathMicrosoftNETLogging },
                }),
            };

            MockPackageAssemblyResolver packageAssemblyResolver = new MockPackageAssemblyResolver
            {
                [packageNewtonsoftJson9] = new List<PackageAssembly>
                {
                    new PackageAssembly(pathNewtonsoftJson9, string.Empty, assemblyNameNewtonsoftJson9),
                },
                [packageNewtonsoftJson12] = new List<PackageAssembly>
                {
                    new PackageAssembly(pathNewtonsoftJson12, string.Empty, assemblyNameNewtonsoftJson12),
                },
            };

            MockAssemblyInformationReader assemblyReferenceReader = new MockAssemblyInformationReader
            {
                [assemblyNameNewtonsoftJson9.FullName] = new List<AssemblyReference>
                {
                    new AssemblyReference(pathMicrosoftNETLogging, assemblyNameMicrosoftNETLogging),
                },
                [assemblyNameMicrosoftNETLogging.FullName] = new List<AssemblyReference>
                {
                    new AssemblyReference(pathMicrosoftNETCommunication, assemblyNameMicrosoftNETCommunication),
                },
                [assemblyNameMicrosoftNETCommunication.FullName] = new List<AssemblyReference>()
                {
                    new AssemblyReference(pathMicrosoftNETTestSdk, assemblyNameMicrosoftNETTestSdk),
                },
                [assemblyNameNewtonsoftJson12.FullName] = new List<AssemblyReference>
                {
                    new AssemblyReference(pathNewtonsoftJsonBson, assemblyNameNewtonsoftJsonBson),
                },
                [assemblyNameNewtonsoftJsonBson.FullName] = new List<AssemblyReference>(),
                [assemblyNameMicrosoftNETTestSdk.FullName] = new List<AssemblyReference>(),
            };

            IInternalsVisibleToReader internalsVisibleToReader = new MockInternalsVisibleToReader
            {
                [pathNewtonsoftJson9] = "Newtonsoft.Json, PublicKey=Some_Public_Key",
            };

            GetAssembliesToShade task = new GetAssembliesToShade(assetsFileLoader, packageAssemblyResolver, assemblyReferenceReader, internalsVisibleToReader)
            {
                IntermediateOutputPath = Path.Combine(TestDirectory, "obj"),
                PackageReferences = packageReferences,
                ProjectAssetsFile = Path.Combine(TestDirectory, "obj", "project.assets.json"),
                References = references,
                ShadedAssemblyKeyFile = Path.Combine(TestAssemblyDirectory, "shaded.snk"),
                TargetFramework = "net472",
                TargetFrameworkMoniker = ".NETFramework,Version=v4.7.2",
            };

            bool result = task.Execute();

            result.ShouldBeTrue();

            ITaskItem[] assembliesToShade = task.AssembliesToShade;

            // TODO: Validate items
        }
    }
}