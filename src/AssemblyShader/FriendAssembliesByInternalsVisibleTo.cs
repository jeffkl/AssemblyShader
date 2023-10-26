// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace AssemblyShader
{
    internal sealed class FriendAssembliesByInternalsVisibleTo : Dictionary<string, List<AssemblyReference>>
    {
        public FriendAssembliesByInternalsVisibleTo()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}