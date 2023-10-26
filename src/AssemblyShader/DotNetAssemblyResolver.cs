// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.IO;
using System.Linq;

namespace AssemblyShader
{
    internal static class DotNetAssemblyResolver
    {
        private static readonly char[] Comma = new[] { ',' };

        private static readonly string? DotNetBasePath = TryFindOnPath(Environment.OSVersion.Platform == PlatformID.Unix ? "dotnet" : "dotnet.exe", out FileInfo? dotnetFileInfo)
            ? dotnetFileInfo!.DirectoryName
            : null;

        private static readonly char[] EqualSign = new[] { '=' };

        private static readonly Version ZeroVersion = new Version(0, 0, 0, 0);

        private enum TargetFrameworkIdentifier
        {
            None,
            NETFramework,
            NETCoreApp,
            NETStandard,
            Silverlight,
            NET,
        }

        internal static bool TryGetReferenceAssemblyPath(string targetFramework, out string? directory)
        {
            directory = null;

            if (DotNetBasePath is null)
            {
                return false;
            }

            (TargetFrameworkIdentifier targetFrameworkMoniker, Version version) = ParseTargetFramework(targetFramework);

            string identifier, identifierExt;

            switch (targetFrameworkMoniker)
            {
                case TargetFrameworkIdentifier.NETCoreApp:
                    identifier = "Microsoft.NETCore.App";
                    identifierExt = $"netcoreapp{version.Major}.{version.Minor}";
                    break;

                case TargetFrameworkIdentifier.NETStandard:
                    identifier = "NETStandard.Library";
                    identifierExt = $"netstandard{version.Major}.{version.Minor}";
                    break;

                case TargetFrameworkIdentifier.NET:
                    identifier = "Microsoft.NETCore.App";
                    identifierExt = $"net{version.Major}.{version.Minor}";
                    break;

                default:
                    return false;
            }

            string basePath = Path.Combine(DotNetBasePath, "packs", $"{identifier}.Ref");

            string? versionFolder = GetClosestVersionFolder(basePath, version);

            if (versionFolder is null)
            {
                return false;
            }

            directory = Path.Combine(basePath, versionFolder, "ref", identifierExt);

            return true;
        }

        private static string? GetClosestVersionFolder(string basePath, Version version)
        {
            (Version? Version, string? DirectoryName) match = new DirectoryInfo(basePath).EnumerateDirectories()
                .Select(i => ParseVersion(i.Name))
                .Where(i => i.Version != null)
                .OrderBy(i => i)
                .FirstOrDefault(i => i.Version >= version);

            return match != default ? match!.DirectoryName : version.ToString();
        }

        private static (TargetFrameworkIdentifier TargetFrameworkIdentifier, Version Version) ParseTargetFramework(string targetFramework)
        {
            if (string.IsNullOrEmpty(targetFramework))
            {
                return (TargetFrameworkIdentifier.NETFramework, ZeroVersion);
            }

            string[] tokens = targetFramework.Split(Comma, 2);

            if (tokens.Length != 2)
            {
                return (TargetFrameworkIdentifier.None, ZeroVersion);
            }

            TargetFrameworkIdentifier identifier;

            switch (tokens[0].Trim().ToUpperInvariant())
            {
                case ".NETCOREAPP":
                    identifier = TargetFrameworkIdentifier.NETCoreApp;
                    break;

                case ".NETSTANDARD":
                    identifier = TargetFrameworkIdentifier.NETStandard;
                    break;

                case "SILVERLIGHT":
                    identifier = TargetFrameworkIdentifier.Silverlight;
                    break;

                default:
                    identifier = TargetFrameworkIdentifier.NETFramework;
                    break;
            }

            Version? version = null;

            for (int i = 1; i < tokens.Length; i++)
            {
                string[] pair = tokens[i].Trim().Split(EqualSign, 2);

                if (pair.Length != 2)
                {
                    continue;
                }

                switch (pair[0].Trim().ToUpperInvariant())
                {
                    case "VERSION":
                        string versionString = pair[1].Trim().TrimStart('v');

                        if ((identifier == TargetFrameworkIdentifier.NETCoreApp || identifier == TargetFrameworkIdentifier.NETStandard) && versionString.Length == 3)
                        {
                            versionString += ".0";
                        }

                        if (!Version.TryParse(versionString, out version))
                        {
                            version = null;
                        }

                        if (version?.Major >= 5 && identifier == TargetFrameworkIdentifier.NETCoreApp)
                        {
                            identifier = TargetFrameworkIdentifier.NET;
                        }

                        break;
                }
            }

            return (identifier, version ?? ZeroVersion);
        }

        private static (Version? Version, string? DirectoryName) ParseVersion(string name)
        {
            try
            {
                return (new Version(RemoveTrailingVersionInfo()), name);
            }
            catch (Exception)
            {
                return (null, null);
            }

            string RemoveTrailingVersionInfo()
            {
                string shortName = name;
                int dashIndex = shortName.IndexOf('-');

                if (dashIndex > 0)
                {
                    shortName = shortName.Remove(dashIndex);
                }

                return shortName;
            }
        }

        private static bool TryFindOnPath(string exe, out FileInfo? fileInfo)
        {
            fileInfo = (Environment.GetEnvironmentVariable("PATH") ?? string.Empty)
                .Split(Path.PathSeparator)
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => new DirectoryInfo(i.Trim()))
                .Where(i => i.Exists)
                .Select(i => new FileInfo(Path.Combine(i.FullName, $"{exe}")))
                .FirstOrDefault(i => i.Exists);

            return fileInfo != null;
        }
    }
}