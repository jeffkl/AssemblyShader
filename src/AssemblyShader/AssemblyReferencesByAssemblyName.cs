// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace AssemblyShader
{
    internal sealed class AssemblyReferencesByAssemblyName : Dictionary<string, List<AssemblyReference>>
    {
        public AssemblyReferencesByAssemblyName()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}