// ------------------------------------------------------------------------------
// <copyright file="Lua.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

/* Not affiliated or endorsed by Blizzard Entertainment */

#pragma warning disable IDE0052, IDE1006, CS0626, SA1601

using War3Net.CodeAnalysis.Common;

namespace War3Api
{
    public static partial class Common
    {
        [NativeLuaMember]
        public static extern int FourCC(string value);
    }
}