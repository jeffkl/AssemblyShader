// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace AssemblyShader.UnitTests
{
    internal sealed class MockInternalsVisibleToReader : Dictionary<string, string>, IInternalsVisibleToReader
    {
        public MockInternalsVisibleToReader()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public string Read(string path)
        {
            if (TryGetValue(path, out string? result))
            {
                return result;
            }

            throw new Exception($"The current mock InternalsVisibleToReader did not specify a value for the assembly '{path}");
        }
    }
}