// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;

namespace AssemblyShader
{
    internal static class ExtensionMethods
    {
        public static string ThrowIfNull(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }

            return value;
        }

        public static T ThrowIfNull<T>(this T obj, string paramName)
            where T : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }
    }
}