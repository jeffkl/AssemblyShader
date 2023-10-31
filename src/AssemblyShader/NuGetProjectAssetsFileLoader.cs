// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using NuGet.Versioning;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace AssemblyShader
{
    internal sealed class NuGetProjectAssetsFileLoader : INuGetProjectAssetsFileLoader
    {
        private static readonly ConcurrentDictionary<FileInfo, (DateTime LastWriteTime, Lazy<NuGetProjectAssetsFile> Lazy)> FileCache = new(FileSystemInfoFullNameEqualityComparer.Instance);

        public NuGetProjectAssetsFile? Load(string projectDirectory, string projectAssetsFile)
        {
            FileInfo fileInfo = new FileInfo(projectAssetsFile);

            if (!fileInfo.Exists)
            {
                return null;
            }

            (DateTime _, Lazy<NuGetProjectAssetsFile> Lazy) cacheEntry = FileCache.AddOrUpdate(
                fileInfo,
                key => (key.LastWriteTime, new Lazy<NuGetProjectAssetsFile>(() => ParseNuGetAssetsFile(projectDirectory, key.FullName))),
                (key, item) =>
                {
                    DateTime lastWriteTime = key.LastWriteTime;

                    if (item.LastWriteTime < lastWriteTime)
                    {
                        return (lastWriteTime, new Lazy<NuGetProjectAssetsFile>(() => ParseNuGetAssetsFile(projectDirectory, key.FullName)));
                    }

                    return item;
                });

            return cacheEntry.Lazy.Value;
        }

        internal NuGetProjectAssetsFile ParseNuGetAssetsFile(string projectDirectory, string projectAssetsFile)
        {
            using FileStream stream = File.OpenRead(projectAssetsFile);

            return ParseNuGetAssetsFile(projectDirectory, stream);
        }

        internal NuGetProjectAssetsFile ParseNuGetAssetsFile(string projectDirectory, Stream stream)
        {
            NuGetProjectAssetsFile assetsFile = new();

            JsonDocumentOptions options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
            };

            using (JsonDocument json = JsonDocument.Parse(stream, options))
            {
                foreach (JsonProperty targetFramework in json.RootElement.GetProperty("targets").EnumerateObject())
                {
                    NuGetProjectAssetsFileSection nuGetProjectAssetsFileSection = new();

                    Dictionary<PackageIdentity, HashSet<PackageIdentity>> packages = nuGetProjectAssetsFileSection.Packages;

                    if (targetFramework.Value.ValueKind == JsonValueKind.Undefined)
                    {
                        continue;
                    }

                    foreach (JsonProperty item in targetFramework.Value.EnumerateObject())
                    {
                        string[] packageDetails = item.Name.Split('/');

                        if (!NuGetVersion.TryParse(packageDetails[1], out NuGetVersion? nuGetVersion))
                        {
                            continue;
                        }

                        PackageIdentity packageIdentity = new PackageIdentity(packageDetails[0], nuGetVersion.ToNormalizedString());

                        if (!packages.TryGetValue(packageIdentity, out HashSet<PackageIdentity>? dependencies))
                        {
                            dependencies = new HashSet<PackageIdentity>();

                            packages[packageIdentity] = dependencies;
                        }

                        if (item.Value.TryGetProperty("dependencies", out JsonElement deps))
                        {
                            foreach (JsonProperty dependency in deps.EnumerateObject())
                            {
                                string? versionString = dependency.Value.GetString();

                                if (versionString is null || !VersionRange.TryParse(versionString, out VersionRange? versionRange) || versionRange?.MinVersion == null)
                                {
                                    continue;
                                }

                                dependencies.Add(new PackageIdentity(dependency.Name, versionRange.MinVersion.ToNormalizedString()));
                            }
                        }
                    }

                    bool added = false;
                    int count = 0;
                    do
                    {
                        count++;
                        added = false;

                        foreach (KeyValuePair<PackageIdentity, HashSet<PackageIdentity>> package in packages)
                        {
                            List<PackageIdentity> dependenciesToAdd = new List<PackageIdentity>();

                            foreach (PackageIdentity dependency in package.Value)
                            {
                                if (packages.TryGetValue(dependency, out HashSet<PackageIdentity>? dependencies))
                                {
                                    dependenciesToAdd.AddRange(dependencies);
                                }
                            }

                            foreach (PackageIdentity dependencyToAdd in dependenciesToAdd)
                            {
                                if (package.Value.Add(dependencyToAdd))
                                {
                                    added = true;
                                }
                            }
                        }
                    }
                    while (added);

                    assetsFile[targetFramework.Name] = nuGetProjectAssetsFileSection;
                }

                foreach (JsonProperty library in json.RootElement.GetProperty("libraries").EnumerateObject())
                {
                    if (!library.Value.TryGetProperty("type", out JsonElement type) || !string.Equals(type.GetString(), "project") || !library.Value.TryGetProperty("path", out JsonElement path))
                    {
                        continue;
                    }

                    string[] libraryDetails = library.Name.Split('/');

                    PackageIdentity packageIdentity = new PackageIdentity(libraryDetails[0], libraryDetails[1]);

                    string? relativePath = path.GetString();

                    if (relativePath is null)
                    {
                        continue;
                    }

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        relativePath = relativePath.Replace('/', '\\');
                    }

                    FileInfo projectFileInfo = new FileInfo(Path.Combine(projectDirectory, relativePath));

                    assetsFile.ProjectReferences[projectFileInfo.FullName] = packageIdentity;
                }
            }

            return assetsFile;
        }
    }
}