// ------------------------------------------------------------------------------
// <copyright file="Lua.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

/* Not affiliated or endorsed by Blizzard Entertainment */

#pragma warning disable IDE0052, IDE1006, CS0626
#pragma warning disable CA1034, CA1707, CA1716, CA2211,
#pragma warning disable SA1201, SA1202, SA1203, SA1300, SA1303, SA1307, SA1310, SA1311, SA1313, SA1401, SA1407, SA1514, SA1516, SA1600, SA1601, SA1604, SA1611, SA1615, SA1626

namespace War3Api
{
    public static partial class Common
    {
        /// @CSharpLua.Template = "FourCC({0})"
        public static extern int FourCC(string value);
        
        /// <summary>
        /// Get a global variable matching the given key.
        /// </summary>
        /// <typeparam name="T">The type of the global variable.</typeparam>
        /// <returns>
        /// The value of global variable
        /// </returns>
        public static T GetGlobal<T>(string key)
        {
            // NOTE: The comments below are important. Please don't remove.
            // The Lua generator will inject the commented code and comment
            // out the C# return statement.

            /*[[
            return rawget(System.global, key)
            --[[
            ]]*/
            return default(T);
            //]]
        }
    }
}
