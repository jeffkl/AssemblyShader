// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace AssemblyShader.UnitTests
{
    internal sealed class MockAssemblyInformationReader : Dictionary<string, List<AssemblyReference>>, IAssemblyInformationReader
    {
        public MockAssemblyInformationReader()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public AssemblyInformation GetAssemblyInformation(IEnumerable<string> assemblyPaths)
        {
            AssemblyInformation assemblyInformation = new AssemblyInformation();

            foreach (KeyValuePair<string, List<AssemblyReference>> i in this)
            {
                assemblyInformation.AssemblyReferences.Add(i.Key, i.Value);
            }

            return assemblyInformation;
        }
    }
}