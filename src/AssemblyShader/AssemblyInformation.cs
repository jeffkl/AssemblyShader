// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

namespace AssemblyShader
{
    internal sealed class AssemblyInformation
    {
        public AssemblyReferencesByAssemblyName AssemblyReferences { get; } = new();

        public FriendAssembliesByInternalsVisibleTo FriendAssemblies { get; } = new();
    }
}