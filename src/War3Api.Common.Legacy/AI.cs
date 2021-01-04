// ------------------------------------------------------------------------------
// <copyright file="AI.cs" company="Drake53">
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
        /// @CSharpLua.Template = "GetUnitCount({0})"
        public static extern int GetUnitCount(int unitid);
        /// @CSharpLua.Template = "GetPlayerUnitTypeCount({0}, {1})"
        public static extern int GetPlayerUnitTypeCount(player p, int unitid);
        /// @CSharpLua.Template = "GetUnitCountDone({0})"
        public static extern int GetUnitCountDone(int unitid);
        /// @CSharpLua.Template = "GetTownUnitCount({0}, {1}, {2})"
        public static extern int GetTownUnitCount(int id, int tn, bool dn);
        /// @CSharpLua.Template = "GetUnitGoldCost({0})"
        public static extern int GetUnitGoldCost(int unitid);
        /// @CSharpLua.Template = "GetUnitWoodCost({0})"
        public static extern int GetUnitWoodCost(int unitid);
        /// @CSharpLua.Template = "GetUnitBuildTime({0})"
        public static extern int GetUnitBuildTime(int unitid);
        /// @CSharpLua.Template = "UnitAlive({0})"
        public static extern bool UnitAlive(unit id);
        /// @CSharpLua.Template = "UnitInvis({0})"
        public static extern bool UnitInvis(unit id);
    }
}