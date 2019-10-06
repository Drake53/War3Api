// ------------------------------------------------------------------------------
// <copyright file="Blizzard.cs" company="Drake53">
// Copyright (c) 2019 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

/* Not affiliated or endorsed by Blizzard Entertainment */

#pragma warning disable IDE0052, IDE1006, CS0626

using War3Net.CodeAnalysis.Common;

using static War3Api.Common;

namespace War3Api
{
    public static partial class Blizzard
    {
        [NativeLuaMemberAttribute]
        public const float bj_PI = 3.14159f;
        [NativeLuaMemberAttribute]
        public const float bj_E = 2.71828f;
        [NativeLuaMemberAttribute]
        public const float bj_CELLWIDTH = 128.0f;
        [NativeLuaMemberAttribute]
        public const float bj_CLIFFHEIGHT = 128.0f;
        [NativeLuaMemberAttribute]
        public const float bj_UNIT_FACING = 270.0f;
        [NativeLuaMemberAttribute]
        public static readonly float bj_RADTODEG = 180.0f / bj_PI;
        [NativeLuaMemberAttribute]
        public static readonly float bj_DEGTORAD = bj_PI / 180.0f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_QUEST = 20.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_QUESTUPDATE = 20.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_QUESTDONE = 20.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_QUESTFAILED = 20.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_QUESTREQUIREMENT = 20.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_MISSIONFAILED = 20.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_ALWAYSHINT = 12.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_HINT = 12.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_SECRET = 10.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_UNITACQUIRED = 15.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_UNITAVAILABLE = 10.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_ITEMACQUIRED = 10.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TEXT_DELAY_WARNING = 12.00f;
        [NativeLuaMemberAttribute]
        public const float bj_QUEUE_DELAY_QUEST = 5.00f;
        [NativeLuaMemberAttribute]
        public const float bj_QUEUE_DELAY_HINT = 5.00f;
        [NativeLuaMemberAttribute]
        public const float bj_QUEUE_DELAY_SECRET = 3.00f;
        [NativeLuaMemberAttribute]
        public const float bj_HANDICAP_EASY = 60.00f;
        [NativeLuaMemberAttribute]
        public const float bj_GAME_STARTED_THRESHOLD = 0.01f;
        [NativeLuaMemberAttribute]
        public const float bj_WAIT_FOR_COND_MIN_INTERVAL = 0.10f;
        [NativeLuaMemberAttribute]
        public const float bj_POLLED_WAIT_INTERVAL = 0.10f;
        [NativeLuaMemberAttribute]
        public const float bj_POLLED_WAIT_SKIP_THRESHOLD = 2.00f;
        [NativeLuaMemberAttribute]
        public const int bj_MAX_INVENTORY = 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MAX_PLAYERS = GetBJMaxPlayers();
        [NativeLuaMemberAttribute]
        public static readonly int bj_PLAYER_NEUTRAL_VICTIM = GetBJPlayerNeutralVictim();
        [NativeLuaMemberAttribute]
        public static readonly int bj_PLAYER_NEUTRAL_EXTRA = GetBJPlayerNeutralExtra();
        [NativeLuaMemberAttribute]
        public static readonly int bj_MAX_PLAYER_SLOTS = GetBJMaxPlayerSlots();
        [NativeLuaMemberAttribute]
        public const int bj_MAX_SKELETONS = 25;
        [NativeLuaMemberAttribute]
        public const int bj_MAX_STOCK_ITEM_SLOTS = 11;
        [NativeLuaMemberAttribute]
        public const int bj_MAX_STOCK_UNIT_SLOTS = 11;
        [NativeLuaMemberAttribute]
        public const int bj_MAX_ITEM_LEVEL = 10;
        [NativeLuaMemberAttribute]
        public const float bj_TOD_DAWN = 6.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TOD_DUSK = 18.00f;
        [NativeLuaMemberAttribute]
        public const float bj_MELEE_STARTING_TOD = 8.00f;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_STARTING_GOLD_V0 = 750;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_STARTING_GOLD_V1 = 500;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_STARTING_LUMBER_V0 = 200;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_STARTING_LUMBER_V1 = 150;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_STARTING_HERO_TOKENS = 1;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_HERO_LIMIT = 3;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_HERO_TYPE_LIMIT = 1;
        [NativeLuaMemberAttribute]
        public const float bj_MELEE_MINE_SEARCH_RADIUS = 2000;
        [NativeLuaMemberAttribute]
        public const float bj_MELEE_CLEAR_UNITS_RADIUS = 1500;
        [NativeLuaMemberAttribute]
        public const float bj_MELEE_CRIPPLE_TIMEOUT = 120.00f;
        [NativeLuaMemberAttribute]
        public const float bj_MELEE_CRIPPLE_MSG_DURATION = 20.00f;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_MAX_TWINKED_HEROES_V0 = 3;
        [NativeLuaMemberAttribute]
        public const int bj_MELEE_MAX_TWINKED_HEROES_V1 = 1;
        [NativeLuaMemberAttribute]
        public const float bj_CREEP_ITEM_DELAY = 0.50f;
        [NativeLuaMemberAttribute]
        public const float bj_STOCK_RESTOCK_INITIAL_DELAY = 120;
        [NativeLuaMemberAttribute]
        public const float bj_STOCK_RESTOCK_INTERVAL = 30;
        [NativeLuaMemberAttribute]
        public const int bj_STOCK_MAX_ITERATIONS = 20;
        [NativeLuaMemberAttribute]
        public const int bj_MAX_DEST_IN_REGION_EVENTS = 64;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_MIN_FARZ = 100;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_DEFAULT_DISTANCE = 1650;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_DEFAULT_FARZ = 5000;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_DEFAULT_AOA = 304;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_DEFAULT_FOV = 70;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_DEFAULT_ROLL = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERA_DEFAULT_ROTATION = 90;
        [NativeLuaMemberAttribute]
        public const float bj_RESCUE_PING_TIME = 2.00f;
        [NativeLuaMemberAttribute]
        public const float bj_NOTHING_SOUND_DURATION = 5.00f;
        [NativeLuaMemberAttribute]
        public const float bj_TRANSMISSION_PING_TIME = 1.00f;
        [NativeLuaMemberAttribute]
        public const int bj_TRANSMISSION_IND_RED = 255;
        [NativeLuaMemberAttribute]
        public const int bj_TRANSMISSION_IND_BLUE = 255;
        [NativeLuaMemberAttribute]
        public const int bj_TRANSMISSION_IND_GREEN = 255;
        [NativeLuaMemberAttribute]
        public const int bj_TRANSMISSION_IND_ALPHA = 255;
        [NativeLuaMemberAttribute]
        public const float bj_TRANSMISSION_PORT_HANGTIME = 1.50f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_INTERFACEFADE = 0.50f;
        [NativeLuaMemberAttribute]
        public static readonly gamespeed bj_CINEMODE_GAMESPEED = MAP_SPEED_NORMAL;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_UNITMOVEMENT = 0.40f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_UNITSOUNDS = 0.00f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_COMBAT = 0.40f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_SPELLS = 0.40f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_UI = 0.00f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_MUSIC = 0.55f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_AMBIENTSOUNDS = 1.00f;
        [NativeLuaMemberAttribute]
        public const float bj_CINEMODE_VOLUME_FIRE = 0.60f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_UNITMOVEMENT = 0.25f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_UNITSOUNDS = 0.00f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_COMBAT = 0.25f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_SPELLS = 0.25f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_UI = 0.00f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_MUSIC = 0.55f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_AMBIENTSOUNDS = 1.00f;
        [NativeLuaMemberAttribute]
        public const float bj_SPEECH_VOLUME_FIRE = 0.60f;
        [NativeLuaMemberAttribute]
        public const float bj_SMARTPAN_TRESHOLD_PAN = 500;
        [NativeLuaMemberAttribute]
        public const float bj_SMARTPAN_TRESHOLD_SNAP = 3500;
        [NativeLuaMemberAttribute]
        public const int bj_MAX_QUEUED_TRIGGERS = 100;
        [NativeLuaMemberAttribute]
        public const float bj_QUEUED_TRIGGER_TIMEOUT = 180.00f;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_T = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_H = 1;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_U = 2;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_O = 3;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_N = 4;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_XN = 5;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_XH = 6;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_XU = 7;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_INDEX_XO = 8;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_T = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_H = 1;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_U = 2;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_O = 3;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_N = 4;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_XN = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_XH = 1;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_XU = 2;
        [NativeLuaMemberAttribute]
        public const int bj_CAMPAIGN_OFFSET_XO = 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_T00 = bj_CAMPAIGN_OFFSET_T * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_T01 = bj_CAMPAIGN_OFFSET_T * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H00 = bj_CAMPAIGN_OFFSET_H * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H01 = bj_CAMPAIGN_OFFSET_H * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H02 = bj_CAMPAIGN_OFFSET_H * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H03 = bj_CAMPAIGN_OFFSET_H * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H04 = bj_CAMPAIGN_OFFSET_H * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H05 = bj_CAMPAIGN_OFFSET_H * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H06 = bj_CAMPAIGN_OFFSET_H * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H07 = bj_CAMPAIGN_OFFSET_H * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H08 = bj_CAMPAIGN_OFFSET_H * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H09 = bj_CAMPAIGN_OFFSET_H * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H10 = bj_CAMPAIGN_OFFSET_H * 1000 + 10;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_H11 = bj_CAMPAIGN_OFFSET_H * 1000 + 11;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U00 = bj_CAMPAIGN_OFFSET_U * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U01 = bj_CAMPAIGN_OFFSET_U * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U02 = bj_CAMPAIGN_OFFSET_U * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U03 = bj_CAMPAIGN_OFFSET_U * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U05 = bj_CAMPAIGN_OFFSET_U * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U07 = bj_CAMPAIGN_OFFSET_U * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U08 = bj_CAMPAIGN_OFFSET_U * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U09 = bj_CAMPAIGN_OFFSET_U * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U10 = bj_CAMPAIGN_OFFSET_U * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_U11 = bj_CAMPAIGN_OFFSET_U * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O00 = bj_CAMPAIGN_OFFSET_O * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O01 = bj_CAMPAIGN_OFFSET_O * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O02 = bj_CAMPAIGN_OFFSET_O * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O03 = bj_CAMPAIGN_OFFSET_O * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O04 = bj_CAMPAIGN_OFFSET_O * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O05 = bj_CAMPAIGN_OFFSET_O * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O06 = bj_CAMPAIGN_OFFSET_O * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O07 = bj_CAMPAIGN_OFFSET_O * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O08 = bj_CAMPAIGN_OFFSET_O * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O09 = bj_CAMPAIGN_OFFSET_O * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_O10 = bj_CAMPAIGN_OFFSET_O * 1000 + 10;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N00 = bj_CAMPAIGN_OFFSET_N * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N01 = bj_CAMPAIGN_OFFSET_N * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N02 = bj_CAMPAIGN_OFFSET_N * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N03 = bj_CAMPAIGN_OFFSET_N * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N04 = bj_CAMPAIGN_OFFSET_N * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N05 = bj_CAMPAIGN_OFFSET_N * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N06 = bj_CAMPAIGN_OFFSET_N * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N07 = bj_CAMPAIGN_OFFSET_N * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N08 = bj_CAMPAIGN_OFFSET_N * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_N09 = bj_CAMPAIGN_OFFSET_N * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN00 = bj_CAMPAIGN_OFFSET_XN * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN01 = bj_CAMPAIGN_OFFSET_XN * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN02 = bj_CAMPAIGN_OFFSET_XN * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN03 = bj_CAMPAIGN_OFFSET_XN * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN04 = bj_CAMPAIGN_OFFSET_XN * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN05 = bj_CAMPAIGN_OFFSET_XN * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN06 = bj_CAMPAIGN_OFFSET_XN * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN07 = bj_CAMPAIGN_OFFSET_XN * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN08 = bj_CAMPAIGN_OFFSET_XN * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN09 = bj_CAMPAIGN_OFFSET_XN * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XN10 = bj_CAMPAIGN_OFFSET_XN * 1000 + 10;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH00 = bj_CAMPAIGN_OFFSET_XH * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH01 = bj_CAMPAIGN_OFFSET_XH * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH02 = bj_CAMPAIGN_OFFSET_XH * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH03 = bj_CAMPAIGN_OFFSET_XH * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH04 = bj_CAMPAIGN_OFFSET_XH * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH05 = bj_CAMPAIGN_OFFSET_XH * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH06 = bj_CAMPAIGN_OFFSET_XH * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH07 = bj_CAMPAIGN_OFFSET_XH * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH08 = bj_CAMPAIGN_OFFSET_XH * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XH09 = bj_CAMPAIGN_OFFSET_XH * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU00 = bj_CAMPAIGN_OFFSET_XU * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU01 = bj_CAMPAIGN_OFFSET_XU * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU02 = bj_CAMPAIGN_OFFSET_XU * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU03 = bj_CAMPAIGN_OFFSET_XU * 1000 + 3;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU04 = bj_CAMPAIGN_OFFSET_XU * 1000 + 4;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU05 = bj_CAMPAIGN_OFFSET_XU * 1000 + 5;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU06 = bj_CAMPAIGN_OFFSET_XU * 1000 + 6;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU07 = bj_CAMPAIGN_OFFSET_XU * 1000 + 7;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU08 = bj_CAMPAIGN_OFFSET_XU * 1000 + 8;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU09 = bj_CAMPAIGN_OFFSET_XU * 1000 + 9;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU10 = bj_CAMPAIGN_OFFSET_XU * 1000 + 10;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU11 = bj_CAMPAIGN_OFFSET_XU * 1000 + 11;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU12 = bj_CAMPAIGN_OFFSET_XU * 1000 + 12;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XU13 = bj_CAMPAIGN_OFFSET_XU * 1000 + 13;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XO00 = bj_CAMPAIGN_OFFSET_XO * 1000 + 0;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XO01 = bj_CAMPAIGN_OFFSET_XO * 1000 + 1;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XO02 = bj_CAMPAIGN_OFFSET_XO * 1000 + 2;
        [NativeLuaMemberAttribute]
        public static readonly int bj_MISSION_INDEX_XO03 = bj_CAMPAIGN_OFFSET_XO * 1000 + 3;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_TOP = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_HOP = 1;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_HED = 2;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_OOP = 3;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_OED = 4;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_UOP = 5;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_UED = 6;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_NOP = 7;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_NED = 8;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_XOP = 9;
        [NativeLuaMemberAttribute]
        public const int bj_CINEMATICINDEX_XED = 10;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_UNALLIED = 0;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_UNALLIED_VISION = 1;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_ALLIED = 2;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_ALLIED_VISION = 3;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_ALLIED_UNITS = 4;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_ALLIED_ADVUNITS = 5;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_NEUTRAL = 6;
        [NativeLuaMemberAttribute]
        public const int bj_ALLIANCE_NEUTRAL_VISION = 7;
        [NativeLuaMemberAttribute]
        public const int bj_KEYEVENTTYPE_DEPRESS = 0;
        [NativeLuaMemberAttribute]
        public const int bj_KEYEVENTTYPE_RELEASE = 1;
        [NativeLuaMemberAttribute]
        public const int bj_KEYEVENTKEY_LEFT = 0;
        [NativeLuaMemberAttribute]
        public const int bj_KEYEVENTKEY_RIGHT = 1;
        [NativeLuaMemberAttribute]
        public const int bj_KEYEVENTKEY_DOWN = 2;
        [NativeLuaMemberAttribute]
        public const int bj_KEYEVENTKEY_UP = 3;
        [NativeLuaMemberAttribute]
        public const int bj_MOUSEEVENTTYPE_DOWN = 0;
        [NativeLuaMemberAttribute]
        public const int bj_MOUSEEVENTTYPE_UP = 1;
        [NativeLuaMemberAttribute]
        public const int bj_MOUSEEVENTTYPE_MOVE = 2;
        [NativeLuaMemberAttribute]
        public const int bj_TIMETYPE_ADD = 0;
        [NativeLuaMemberAttribute]
        public const int bj_TIMETYPE_SET = 1;
        [NativeLuaMemberAttribute]
        public const int bj_TIMETYPE_SUB = 2;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERABOUNDS_ADJUST_ADD = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CAMERABOUNDS_ADJUST_SUB = 1;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTTYPE_REQ_DISCOVERED = 0;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTTYPE_REQ_UNDISCOVERED = 1;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTTYPE_OPT_DISCOVERED = 2;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTTYPE_OPT_UNDISCOVERED = 3;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_DISCOVERED = 0;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_UPDATED = 1;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_COMPLETED = 2;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_FAILED = 3;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_REQUIREMENT = 4;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_MISSIONFAILED = 5;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_ALWAYSHINT = 6;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_HINT = 7;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_SECRET = 8;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_UNITACQUIRED = 9;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_UNITAVAILABLE = 10;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_ITEMACQUIRED = 11;
        [NativeLuaMemberAttribute]
        public const int bj_QUESTMESSAGE_WARNING = 12;
        [NativeLuaMemberAttribute]
        public const int bj_SORTTYPE_SORTBYVALUE = 0;
        [NativeLuaMemberAttribute]
        public const int bj_SORTTYPE_SORTBYPLAYER = 1;
        [NativeLuaMemberAttribute]
        public const int bj_SORTTYPE_SORTBYLABEL = 2;
        [NativeLuaMemberAttribute]
        public const int bj_CINEFADETYPE_FADEIN = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CINEFADETYPE_FADEOUT = 1;
        [NativeLuaMemberAttribute]
        public const int bj_CINEFADETYPE_FADEOUTIN = 2;
        [NativeLuaMemberAttribute]
        public const int bj_REMOVEBUFFS_POSITIVE = 0;
        [NativeLuaMemberAttribute]
        public const int bj_REMOVEBUFFS_NEGATIVE = 1;
        [NativeLuaMemberAttribute]
        public const int bj_REMOVEBUFFS_ALL = 2;
        [NativeLuaMemberAttribute]
        public const int bj_REMOVEBUFFS_NONTLIFE = 3;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_POLARITY_POSITIVE = 0;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_POLARITY_NEGATIVE = 1;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_POLARITY_EITHER = 2;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_RESIST_MAGIC = 0;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_RESIST_PHYSICAL = 1;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_RESIST_EITHER = 2;
        [NativeLuaMemberAttribute]
        public const int bj_BUFF_RESIST_BOTH = 3;
        [NativeLuaMemberAttribute]
        public const int bj_HEROSTAT_STR = 0;
        [NativeLuaMemberAttribute]
        public const int bj_HEROSTAT_AGI = 1;
        [NativeLuaMemberAttribute]
        public const int bj_HEROSTAT_INT = 2;
        [NativeLuaMemberAttribute]
        public const int bj_MODIFYMETHOD_ADD = 0;
        [NativeLuaMemberAttribute]
        public const int bj_MODIFYMETHOD_SUB = 1;
        [NativeLuaMemberAttribute]
        public const int bj_MODIFYMETHOD_SET = 2;
        [NativeLuaMemberAttribute]
        public const int bj_UNIT_STATE_METHOD_ABSOLUTE = 0;
        [NativeLuaMemberAttribute]
        public const int bj_UNIT_STATE_METHOD_RELATIVE = 1;
        [NativeLuaMemberAttribute]
        public const int bj_UNIT_STATE_METHOD_DEFAULTS = 2;
        [NativeLuaMemberAttribute]
        public const int bj_UNIT_STATE_METHOD_MAXIMUM = 3;
        [NativeLuaMemberAttribute]
        public const int bj_GATEOPERATION_CLOSE = 0;
        [NativeLuaMemberAttribute]
        public const int bj_GATEOPERATION_OPEN = 1;
        [NativeLuaMemberAttribute]
        public const int bj_GATEOPERATION_DESTROY = 2;
        [NativeLuaMemberAttribute]
        public const int bj_GAMECACHE_BOOLEAN = 0;
        [NativeLuaMemberAttribute]
        public const int bj_GAMECACHE_INTEGER = 1;
        [NativeLuaMemberAttribute]
        public const int bj_GAMECACHE_REAL = 2;
        [NativeLuaMemberAttribute]
        public const int bj_GAMECACHE_UNIT = 3;
        [NativeLuaMemberAttribute]
        public const int bj_GAMECACHE_STRING = 4;
        [NativeLuaMemberAttribute]
        public const int bj_HASHTABLE_BOOLEAN = 0;
        [NativeLuaMemberAttribute]
        public const int bj_HASHTABLE_INTEGER = 1;
        [NativeLuaMemberAttribute]
        public const int bj_HASHTABLE_REAL = 2;
        [NativeLuaMemberAttribute]
        public const int bj_HASHTABLE_STRING = 3;
        [NativeLuaMemberAttribute]
        public const int bj_HASHTABLE_HANDLE = 4;
        [NativeLuaMemberAttribute]
        public const int bj_ITEM_STATUS_HIDDEN = 0;
        [NativeLuaMemberAttribute]
        public const int bj_ITEM_STATUS_OWNED = 1;
        [NativeLuaMemberAttribute]
        public const int bj_ITEM_STATUS_INVULNERABLE = 2;
        [NativeLuaMemberAttribute]
        public const int bj_ITEM_STATUS_POWERUP = 3;
        [NativeLuaMemberAttribute]
        public const int bj_ITEM_STATUS_SELLABLE = 4;
        [NativeLuaMemberAttribute]
        public const int bj_ITEM_STATUS_PAWNABLE = 5;
        [NativeLuaMemberAttribute]
        public const int bj_ITEMCODE_STATUS_POWERUP = 0;
        [NativeLuaMemberAttribute]
        public const int bj_ITEMCODE_STATUS_SELLABLE = 1;
        [NativeLuaMemberAttribute]
        public const int bj_ITEMCODE_STATUS_PAWNABLE = 2;
        [NativeLuaMemberAttribute]
        public const int bj_MINIMAPPINGSTYLE_SIMPLE = 0;
        [NativeLuaMemberAttribute]
        public const int bj_MINIMAPPINGSTYLE_FLASHY = 1;
        [NativeLuaMemberAttribute]
        public const int bj_MINIMAPPINGSTYLE_ATTACK = 2;
        [NativeLuaMemberAttribute]
        public const float bj_CORPSE_MAX_DEATH_TIME = 8.00f;
        [NativeLuaMemberAttribute]
        public const int bj_CORPSETYPE_FLESH = 0;
        [NativeLuaMemberAttribute]
        public const int bj_CORPSETYPE_BONE = 1;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_BLOCKER_CODE = 1146381680;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_CODE01 = 1146384998;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_CODE02 = 1146385016;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_WALL_TYPE_ALL = 0;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_WALL_TYPE_EAST = 1;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_WALL_TYPE_NORTH = 2;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_WALL_TYPE_SOUTH = 3;
        [NativeLuaMemberAttribute]
        public const int bj_ELEVATOR_WALL_TYPE_WEST = 4;
        [NativeLuaMemberAttribute]
        public static force bj_FORCE_ALL_PLAYERS = null;
        [NativeLuaMemberAttribute]
        public static force[] bj_FORCE_PLAYER = new force[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static int bj_MELEE_MAX_TWINKED_HEROES = 0;
        [NativeLuaMemberAttribute]
        public static rect bj_mapInitialPlayableArea = null;
        [NativeLuaMemberAttribute]
        public static rect bj_mapInitialCameraBounds = null;
        [NativeLuaMemberAttribute]
        public static int bj_forLoopAIndex = 0;
        [NativeLuaMemberAttribute]
        public static int bj_forLoopBIndex = 0;
        [NativeLuaMemberAttribute]
        public static int bj_forLoopAIndexEnd = 0;
        [NativeLuaMemberAttribute]
        public static int bj_forLoopBIndexEnd = 0;
        [NativeLuaMemberAttribute]
        public static bool bj_slotControlReady = false;
        [NativeLuaMemberAttribute]
        public static bool[] bj_slotControlUsed = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static mapcontrol[] bj_slotControl = new mapcontrol[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static timer bj_gameStartedTimer = null;
        [NativeLuaMemberAttribute]
        public static bool bj_gameStarted = false;
        [NativeLuaMemberAttribute]
        public static timer bj_volumeGroupsTimer = CreateTimer();
        [NativeLuaMemberAttribute]
        public static bool bj_isSinglePlayer = false;
        [NativeLuaMemberAttribute]
        public static trigger bj_dncSoundsDay = null;
        [NativeLuaMemberAttribute]
        public static trigger bj_dncSoundsNight = null;
        [NativeLuaMemberAttribute]
        public static sound bj_dayAmbientSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_nightAmbientSound = null;
        [NativeLuaMemberAttribute]
        public static trigger bj_dncSoundsDawn = null;
        [NativeLuaMemberAttribute]
        public static trigger bj_dncSoundsDusk = null;
        [NativeLuaMemberAttribute]
        public static sound bj_dawnSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_duskSound = null;
        [NativeLuaMemberAttribute]
        public static bool bj_useDawnDuskSounds = true;
        [NativeLuaMemberAttribute]
        public static bool bj_dncIsDaytime = false;
        [NativeLuaMemberAttribute]
        public static sound bj_rescueSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questDiscoveredSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questUpdatedSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questCompletedSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questFailedSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questHintSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questSecretSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questItemAcquiredSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_questWarningSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_victoryDialogSound = null;
        [NativeLuaMemberAttribute]
        public static sound bj_defeatDialogSound = null;
        [NativeLuaMemberAttribute]
        public static trigger bj_stockItemPurchased = null;
        [NativeLuaMemberAttribute]
        public static timer bj_stockUpdateTimer = null;
        [NativeLuaMemberAttribute]
        public static bool[] bj_stockAllowedPermanent = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool[] bj_stockAllowedCharged = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool[] bj_stockAllowedArtifact = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static int bj_stockPickedItemLevel = 0;
        [NativeLuaMemberAttribute]
        public static itemtype bj_stockPickedItemType = default;
        [NativeLuaMemberAttribute]
        public static trigger bj_meleeVisibilityTrained = null;
        [NativeLuaMemberAttribute]
        public static bool bj_meleeVisibilityIsDay = true;
        [NativeLuaMemberAttribute]
        public static bool bj_meleeGrantHeroItems = false;
        [NativeLuaMemberAttribute]
        public static location bj_meleeNearestMineToLoc = null;
        [NativeLuaMemberAttribute]
        public static unit bj_meleeNearestMine = null;
        [NativeLuaMemberAttribute]
        public static float bj_meleeNearestMineDist = 0.00f;
        [NativeLuaMemberAttribute]
        public static bool bj_meleeGameOver = false;
        [NativeLuaMemberAttribute]
        public static bool[] bj_meleeDefeated = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool[] bj_meleeVictoried = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static unit[] bj_ghoul = new unit[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static timer[] bj_crippledTimer = new timer[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static timerdialog[] bj_crippledTimerWindows = new timerdialog[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool[] bj_playerIsCrippled = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool[] bj_playerIsExposed = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool bj_finishSoonAllExposed = false;
        [NativeLuaMemberAttribute]
        public static timerdialog bj_finishSoonTimerDialog = null;
        [NativeLuaMemberAttribute]
        public static int[] bj_meleeTwinkedHeroes = new int[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static trigger bj_rescueUnitBehavior = null;
        [NativeLuaMemberAttribute]
        public static bool bj_rescueChangeColorUnit = true;
        [NativeLuaMemberAttribute]
        public static bool bj_rescueChangeColorBldg = true;
        [NativeLuaMemberAttribute]
        public static timer bj_cineSceneEndingTimer = null;
        [NativeLuaMemberAttribute]
        public static sound bj_cineSceneLastSound = null;
        [NativeLuaMemberAttribute]
        public static trigger bj_cineSceneBeingSkipped = null;
        [NativeLuaMemberAttribute]
        public static gamespeed bj_cineModePriorSpeed = MAP_SPEED_NORMAL;
        [NativeLuaMemberAttribute]
        public static bool bj_cineModePriorFogSetting = false;
        [NativeLuaMemberAttribute]
        public static bool bj_cineModePriorMaskSetting = false;
        [NativeLuaMemberAttribute]
        public static bool bj_cineModeAlreadyIn = false;
        [NativeLuaMemberAttribute]
        public static bool bj_cineModePriorDawnDusk = false;
        [NativeLuaMemberAttribute]
        public static int bj_cineModeSavedSeed = 0;
        [NativeLuaMemberAttribute]
        public static timer bj_cineFadeFinishTimer = null;
        [NativeLuaMemberAttribute]
        public static timer bj_cineFadeContinueTimer = null;
        [NativeLuaMemberAttribute]
        public static float bj_cineFadeContinueRed = 0;
        [NativeLuaMemberAttribute]
        public static float bj_cineFadeContinueGreen = 0;
        [NativeLuaMemberAttribute]
        public static float bj_cineFadeContinueBlue = 0;
        [NativeLuaMemberAttribute]
        public static float bj_cineFadeContinueTrans = 0;
        [NativeLuaMemberAttribute]
        public static float bj_cineFadeContinueDuration = 0;
        [NativeLuaMemberAttribute]
        public static string bj_cineFadeContinueTex = string.Empty;
        [NativeLuaMemberAttribute]
        public static int bj_queuedExecTotal = 0;
        [NativeLuaMemberAttribute]
        public static trigger[] bj_queuedExecTriggers = new trigger[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static bool[] bj_queuedExecUseConds = new bool[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static timer bj_queuedExecTimeoutTimer = CreateTimer();
        [NativeLuaMemberAttribute]
        public static trigger bj_queuedExecTimeout = null;
        [NativeLuaMemberAttribute]
        public static int bj_destInRegionDiesCount = 0;
        [NativeLuaMemberAttribute]
        public static trigger bj_destInRegionDiesTrig = null;
        [NativeLuaMemberAttribute]
        public static int bj_groupCountUnits = 0;
        [NativeLuaMemberAttribute]
        public static int bj_forceCountPlayers = 0;
        [NativeLuaMemberAttribute]
        public static int bj_groupEnumTypeId = 0;
        [NativeLuaMemberAttribute]
        public static player bj_groupEnumOwningPlayer = null;
        [NativeLuaMemberAttribute]
        public static group bj_groupAddGroupDest = null;
        [NativeLuaMemberAttribute]
        public static group bj_groupRemoveGroupDest = null;
        [NativeLuaMemberAttribute]
        public static int bj_groupRandomConsidered = 0;
        [NativeLuaMemberAttribute]
        public static unit bj_groupRandomCurrentPick = null;
        [NativeLuaMemberAttribute]
        public static group bj_groupLastCreatedDest = null;
        [NativeLuaMemberAttribute]
        public static group bj_randomSubGroupGroup = null;
        [NativeLuaMemberAttribute]
        public static int bj_randomSubGroupWant = 0;
        [NativeLuaMemberAttribute]
        public static int bj_randomSubGroupTotal = 0;
        [NativeLuaMemberAttribute]
        public static float bj_randomSubGroupChance = 0;
        [NativeLuaMemberAttribute]
        public static int bj_destRandomConsidered = 0;
        [NativeLuaMemberAttribute]
        public static destructable bj_destRandomCurrentPick = null;
        [NativeLuaMemberAttribute]
        public static destructable bj_elevatorWallBlocker = null;
        [NativeLuaMemberAttribute]
        public static destructable bj_elevatorNeighbor = null;
        [NativeLuaMemberAttribute]
        public static int bj_itemRandomConsidered = 0;
        [NativeLuaMemberAttribute]
        public static item bj_itemRandomCurrentPick = null;
        [NativeLuaMemberAttribute]
        public static int bj_forceRandomConsidered = 0;
        [NativeLuaMemberAttribute]
        public static player bj_forceRandomCurrentPick = null;
        [NativeLuaMemberAttribute]
        public static unit bj_makeUnitRescuableUnit = null;
        [NativeLuaMemberAttribute]
        public static bool bj_makeUnitRescuableFlag = true;
        [NativeLuaMemberAttribute]
        public static bool bj_pauseAllUnitsFlag = true;
        [NativeLuaMemberAttribute]
        public static location bj_enumDestructableCenter = null;
        [NativeLuaMemberAttribute]
        public static float bj_enumDestructableRadius = 0;
        [NativeLuaMemberAttribute]
        public static playercolor bj_setPlayerTargetColor = null;
        [NativeLuaMemberAttribute]
        public static bool bj_isUnitGroupDeadResult = true;
        [NativeLuaMemberAttribute]
        public static bool bj_isUnitGroupEmptyResult = true;
        [NativeLuaMemberAttribute]
        public static bool bj_isUnitGroupInRectResult = true;
        [NativeLuaMemberAttribute]
        public static rect bj_isUnitGroupInRectRect = null;
        [NativeLuaMemberAttribute]
        public static bool bj_changeLevelShowScores = false;
        [NativeLuaMemberAttribute]
        public static string bj_changeLevelMapName = null;
        [NativeLuaMemberAttribute]
        public static group bj_suspendDecayFleshGroup = CreateGroup();
        [NativeLuaMemberAttribute]
        public static group bj_suspendDecayBoneGroup = CreateGroup();
        [NativeLuaMemberAttribute]
        public static timer bj_delayedSuspendDecayTimer = CreateTimer();
        [NativeLuaMemberAttribute]
        public static trigger bj_delayedSuspendDecayTrig = null;
        [NativeLuaMemberAttribute]
        public static int bj_livingPlayerUnitsTypeId = 0;
        [NativeLuaMemberAttribute]
        public static widget bj_lastDyingWidget = null;
        [NativeLuaMemberAttribute]
        public static int bj_randDistCount = 0;
        [NativeLuaMemberAttribute]
        public static int[] bj_randDistID = new int[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static int[] bj_randDistChance = new int[JASS_MAX_ARRAY_SIZE];
        [NativeLuaMemberAttribute]
        public static unit bj_lastCreatedUnit = null;
        [NativeLuaMemberAttribute]
        public static item bj_lastCreatedItem = null;
        [NativeLuaMemberAttribute]
        public static item bj_lastRemovedItem = null;
        [NativeLuaMemberAttribute]
        public static unit bj_lastHauntedGoldMine = null;
        [NativeLuaMemberAttribute]
        public static destructable bj_lastCreatedDestructable = null;
        [NativeLuaMemberAttribute]
        public static group bj_lastCreatedGroup = CreateGroup();
        [NativeLuaMemberAttribute]
        public static fogmodifier bj_lastCreatedFogModifier = null;
        [NativeLuaMemberAttribute]
        public static effect bj_lastCreatedEffect = null;
        [NativeLuaMemberAttribute]
        public static weathereffect bj_lastCreatedWeatherEffect = null;
        [NativeLuaMemberAttribute]
        public static terraindeformation bj_lastCreatedTerrainDeformation = null;
        [NativeLuaMemberAttribute]
        public static quest bj_lastCreatedQuest = null;
        [NativeLuaMemberAttribute]
        public static questitem bj_lastCreatedQuestItem = null;
        [NativeLuaMemberAttribute]
        public static defeatcondition bj_lastCreatedDefeatCondition = null;
        [NativeLuaMemberAttribute]
        public static timer bj_lastStartedTimer = CreateTimer();
        [NativeLuaMemberAttribute]
        public static timerdialog bj_lastCreatedTimerDialog = null;
        [NativeLuaMemberAttribute]
        public static leaderboard bj_lastCreatedLeaderboard = null;
        [NativeLuaMemberAttribute]
        public static multiboard bj_lastCreatedMultiboard = null;
        [NativeLuaMemberAttribute]
        public static sound bj_lastPlayedSound = null;
        [NativeLuaMemberAttribute]
        public static string bj_lastPlayedMusic = string.Empty;
        [NativeLuaMemberAttribute]
        public static float bj_lastTransmissionDuration = 0;
        [NativeLuaMemberAttribute]
        public static gamecache bj_lastCreatedGameCache = null;
        [NativeLuaMemberAttribute]
        public static hashtable bj_lastCreatedHashtable = null;
        [NativeLuaMemberAttribute]
        public static unit bj_lastLoadedUnit = null;
        [NativeLuaMemberAttribute]
        public static button bj_lastCreatedButton = null;
        [NativeLuaMemberAttribute]
        public static unit bj_lastReplacedUnit = null;
        [NativeLuaMemberAttribute]
        public static texttag bj_lastCreatedTextTag = null;
        [NativeLuaMemberAttribute]
        public static lightning bj_lastCreatedLightning = null;
        [NativeLuaMemberAttribute]
        public static image bj_lastCreatedImage = null;
        [NativeLuaMemberAttribute]
        public static ubersplat bj_lastCreatedUbersplat = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterIssueHauntOrderAtLocBJ = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterEnumDestructablesInCircleBJ = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterGetUnitsInRectOfPlayer = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterGetUnitsOfTypeIdAll = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterGetUnitsOfPlayerAndTypeId = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterMeleeTrainedUnitIsHeroBJ = null;
        [NativeLuaMemberAttribute]
        public static boolexpr filterLivingPlayerUnitsOfTypeId = null;
        [NativeLuaMemberAttribute]
        public static bool bj_wantDestroyGroup = false;
        [NativeLuaMemberAttribute]
        public static bool bj_lastInstObjFuncSuccessful = true;
        [NativeLuaMemberAttribute]
        public static extern void BJDebugMsg(string msg);
        [NativeLuaMemberAttribute]
        public static extern float RMinBJ(float a, float b);
        [NativeLuaMemberAttribute]
        public static extern float RMaxBJ(float a, float b);
        [NativeLuaMemberAttribute]
        public static extern float RAbsBJ(float a);
        [NativeLuaMemberAttribute]
        public static extern float RSignBJ(float a);
        [NativeLuaMemberAttribute]
        public static extern int IMinBJ(int a, int b);
        [NativeLuaMemberAttribute]
        public static extern int IMaxBJ(int a, int b);
        [NativeLuaMemberAttribute]
        public static extern int IAbsBJ(int a);
        [NativeLuaMemberAttribute]
        public static extern int ISignBJ(int a);
        [NativeLuaMemberAttribute]
        public static extern float SinBJ(float degrees);
        [NativeLuaMemberAttribute]
        public static extern float CosBJ(float degrees);
        [NativeLuaMemberAttribute]
        public static extern float TanBJ(float degrees);
        [NativeLuaMemberAttribute]
        public static extern float AsinBJ(float degrees);
        [NativeLuaMemberAttribute]
        public static extern float AcosBJ(float degrees);
        [NativeLuaMemberAttribute]
        public static extern float AtanBJ(float degrees);
        [NativeLuaMemberAttribute]
        public static extern float Atan2BJ(float y, float x);
        [NativeLuaMemberAttribute]
        public static extern float AngleBetweenPoints(location locA, location locB);
        [NativeLuaMemberAttribute]
        public static extern float DistanceBetweenPoints(location locA, location locB);
        [NativeLuaMemberAttribute]
        public static extern location PolarProjectionBJ(location source, float dist, float angle);
        [NativeLuaMemberAttribute]
        public static extern float GetRandomDirectionDeg();
        [NativeLuaMemberAttribute]
        public static extern float GetRandomPercentageBJ();
        [NativeLuaMemberAttribute]
        public static extern location GetRandomLocInRect(rect whichRect);
        [NativeLuaMemberAttribute]
        public static extern int ModuloInteger(int dividend, int divisor);
        [NativeLuaMemberAttribute]
        public static extern float ModuloReal(float dividend, float divisor);
        [NativeLuaMemberAttribute]
        public static extern location OffsetLocation(location loc, float dx, float dy);
        [NativeLuaMemberAttribute]
        public static extern rect OffsetRectBJ(rect r, float dx, float dy);
        [NativeLuaMemberAttribute]
        public static extern rect RectFromCenterSizeBJ(location center, float width, float height);
        [NativeLuaMemberAttribute]
        public static extern bool RectContainsCoords(rect r, float x, float y);
        [NativeLuaMemberAttribute]
        public static extern bool RectContainsLoc(rect r, location loc);
        [NativeLuaMemberAttribute]
        public static extern bool RectContainsUnit(rect r, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool RectContainsItem(item whichItem, rect r);
        [NativeLuaMemberAttribute]
        public static extern void ConditionalTriggerExecute(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern bool TriggerExecuteBJ(trigger trig, bool checkConditions);
        [NativeLuaMemberAttribute]
        public static extern bool PostTriggerExecuteBJ(trigger trig, bool checkConditions);
        [NativeLuaMemberAttribute]
        public static extern void QueuedTriggerCheck();
        [NativeLuaMemberAttribute]
        public static extern int QueuedTriggerGetIndex(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern bool QueuedTriggerRemoveByIndex(int trigIndex);
        [NativeLuaMemberAttribute]
        public static extern bool QueuedTriggerAttemptExec();
        [NativeLuaMemberAttribute]
        public static extern bool QueuedTriggerAddBJ(trigger trig, bool checkConditions);
        [NativeLuaMemberAttribute]
        public static extern void QueuedTriggerRemoveBJ(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern void QueuedTriggerDoneBJ();
        [NativeLuaMemberAttribute]
        public static extern void QueuedTriggerClearBJ();
        [NativeLuaMemberAttribute]
        public static extern void QueuedTriggerClearInactiveBJ();
        [NativeLuaMemberAttribute]
        public static extern int QueuedTriggerCountBJ();
        [NativeLuaMemberAttribute]
        public static extern bool IsTriggerQueueEmptyBJ();
        [NativeLuaMemberAttribute]
        public static extern bool IsTriggerQueuedBJ(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern int GetForLoopIndexA();
        [NativeLuaMemberAttribute]
        public static extern void SetForLoopIndexA(int newIndex);
        [NativeLuaMemberAttribute]
        public static extern int GetForLoopIndexB();
        [NativeLuaMemberAttribute]
        public static extern void SetForLoopIndexB(int newIndex);
        [NativeLuaMemberAttribute]
        public static extern void PolledWait(float duration);
        [NativeLuaMemberAttribute]
        public static extern int IntegerTertiaryOp(bool flag, int valueA, int valueB);
        [NativeLuaMemberAttribute]
        public static extern void DoNothing();
        [NativeLuaMemberAttribute]
        public static extern void CommentString(string commentString);
        [NativeLuaMemberAttribute]
        public static extern string StringIdentity(string theString);
        [NativeLuaMemberAttribute]
        public static extern bool GetBooleanAnd(bool valueA, bool valueB);
        [NativeLuaMemberAttribute]
        public static extern bool GetBooleanOr(bool valueA, bool valueB);
        [NativeLuaMemberAttribute]
        public static extern int PercentToInt(float percentage, int max);
        [NativeLuaMemberAttribute]
        public static extern int PercentTo255(float percentage);
        [NativeLuaMemberAttribute]
        public static extern float GetTimeOfDay();
        [NativeLuaMemberAttribute]
        public static extern void SetTimeOfDay(float whatTime);
        [NativeLuaMemberAttribute]
        public static extern void SetTimeOfDayScalePercentBJ(float scalePercent);
        [NativeLuaMemberAttribute]
        public static extern float GetTimeOfDayScalePercentBJ();
        [NativeLuaMemberAttribute]
        public static extern void PlaySound(string soundName);
        [NativeLuaMemberAttribute]
        public static extern bool CompareLocationsBJ(location A, location B);
        [NativeLuaMemberAttribute]
        public static extern bool CompareRectsBJ(rect A, rect B);
        [NativeLuaMemberAttribute]
        public static extern rect GetRectFromCircleBJ(location center, float radius);
        [NativeLuaMemberAttribute]
        public static extern camerasetup GetCurrentCameraSetup();
        [NativeLuaMemberAttribute]
        public static extern void CameraSetupApplyForPlayer(bool doPan, camerasetup whichSetup, player whichPlayer, float duration);
        [NativeLuaMemberAttribute]
        public static extern float CameraSetupGetFieldSwap(camerafield whichField, camerasetup whichSetup);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraFieldForPlayer(player whichPlayer, camerafield whichField, float value, float duration);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraTargetControllerNoZForPlayer(player whichPlayer, unit whichUnit, float xoffset, float yoffset, bool inheritOrientation);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraPositionForPlayer(player whichPlayer, float x, float y);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraPositionLocForPlayer(player whichPlayer, location loc);
        [NativeLuaMemberAttribute]
        public static extern void RotateCameraAroundLocBJ(float degrees, location loc, player whichPlayer, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PanCameraToForPlayer(player whichPlayer, float x, float y);
        [NativeLuaMemberAttribute]
        public static extern void PanCameraToLocForPlayer(player whichPlayer, location loc);
        [NativeLuaMemberAttribute]
        public static extern void PanCameraToTimedForPlayer(player whichPlayer, float x, float y, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PanCameraToTimedLocForPlayer(player whichPlayer, location loc, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PanCameraToTimedLocWithZForPlayer(player whichPlayer, location loc, float zOffset, float duration);
        [NativeLuaMemberAttribute]
        public static extern void SmartCameraPanBJ(player whichPlayer, location loc, float duration);
        [NativeLuaMemberAttribute]
        public static extern void SetCinematicCameraForPlayer(player whichPlayer, string cameraModelFile);
        [NativeLuaMemberAttribute]
        public static extern void ResetToGameCameraForPlayer(player whichPlayer, float duration);
        [NativeLuaMemberAttribute]
        public static extern void CameraSetSourceNoiseForPlayer(player whichPlayer, float magnitude, float velocity);
        [NativeLuaMemberAttribute]
        public static extern void CameraSetTargetNoiseForPlayer(player whichPlayer, float magnitude, float velocity);
        [NativeLuaMemberAttribute]
        public static extern void CameraSetEQNoiseForPlayer(player whichPlayer, float magnitude);
        [NativeLuaMemberAttribute]
        public static extern void CameraClearNoiseForPlayer(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern rect GetCurrentCameraBoundsMapRectBJ();
        [NativeLuaMemberAttribute]
        public static extern rect GetCameraBoundsMapRect();
        [NativeLuaMemberAttribute]
        public static extern rect GetPlayableMapRect();
        [NativeLuaMemberAttribute]
        public static extern rect GetEntireMapRect();
        [NativeLuaMemberAttribute]
        public static extern void SetCameraBoundsToRect(rect r);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraBoundsToRectForPlayerBJ(player whichPlayer, rect r);
        [NativeLuaMemberAttribute]
        public static extern void AdjustCameraBoundsBJ(int adjustMethod, float dxWest, float dxEast, float dyNorth, float dySouth);
        [NativeLuaMemberAttribute]
        public static extern void AdjustCameraBoundsForPlayerBJ(int adjustMethod, player whichPlayer, float dxWest, float dxEast, float dyNorth, float dySouth);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraQuickPositionForPlayer(player whichPlayer, float x, float y);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraQuickPositionLocForPlayer(player whichPlayer, location loc);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraQuickPositionLoc(location loc);
        [NativeLuaMemberAttribute]
        public static extern void StopCameraForPlayerBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetCameraOrientControllerForPlayerBJ(player whichPlayer, unit whichUnit, float xoffset, float yoffset);
        [NativeLuaMemberAttribute]
        public static extern void CameraSetSmoothingFactorBJ(float factor);
        [NativeLuaMemberAttribute]
        public static extern void CameraResetSmoothingFactorBJ();
        [NativeLuaMemberAttribute]
        public static extern void DisplayTextToForce(force toForce, string message);
        [NativeLuaMemberAttribute]
        public static extern void DisplayTimedTextToForce(force toForce, float duration, string message);
        [NativeLuaMemberAttribute]
        public static extern void ClearTextMessagesBJ(force toForce);
        [NativeLuaMemberAttribute]
        public static extern string SubStringBJ(string source, int start, int end);
        [NativeLuaMemberAttribute]
        public static extern int GetHandleIdBJ(object h);
        [NativeLuaMemberAttribute]
        public static extern int StringHashBJ(string s);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterTimerEventPeriodic(trigger trig, float timeout);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterTimerEventSingle(trigger trig, float timeout);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterTimerExpireEventBJ(trigger trig, timer t);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerUnitEventSimple(trigger trig, player whichPlayer, playerunitevent whichEvent);
        [NativeLuaMemberAttribute]
        public static extern void TriggerRegisterAnyUnitEventBJ(trigger trig, playerunitevent whichEvent);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerSelectionEventBJ(trigger trig, player whichPlayer, bool selected);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerKeyEventBJ(trigger trig, player whichPlayer, int keType, int keKey);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerMouseEventBJ(trigger trig, player whichPlayer, int meType);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerEventVictory(trigger trig, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerEventDefeat(trigger trig, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerEventLeave(trigger trig, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerEventAllianceChanged(trigger trig, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterPlayerEventEndCinematic(trigger trig, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterGameStateEventTimeOfDay(trigger trig, limitop opcode, float limitval);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterEnterRegionSimple(trigger trig, region whichRegion);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterLeaveRegionSimple(trigger trig, region whichRegion);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterEnterRectSimple(trigger trig, rect r);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterLeaveRectSimple(trigger trig, rect r);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterDistanceBetweenUnits(trigger trig, unit whichUnit, boolexpr condition, float range);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterUnitInRangeSimple(trigger trig, float range, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterUnitLifeEvent(trigger trig, unit whichUnit, limitop opcode, float limitval);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterUnitManaEvent(trigger trig, unit whichUnit, limitop opcode, float limitval);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterDialogEventBJ(trigger trig, dialog whichDialog);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterShowSkillEventBJ(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterBuildSubmenuEventBJ(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterGameLoadedEventBJ(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern @event TriggerRegisterGameSavedEventBJ(trigger trig);
        [NativeLuaMemberAttribute]
        public static extern void RegisterDestDeathInRegionEnum();
        [NativeLuaMemberAttribute]
        public static extern void TriggerRegisterDestDeathInRegionEvent(trigger trig, rect r);
        [NativeLuaMemberAttribute]
        public static extern weathereffect AddWeatherEffectSaveLast(rect where, int effectID);
        [NativeLuaMemberAttribute]
        public static extern weathereffect GetLastCreatedWeatherEffect();
        [NativeLuaMemberAttribute]
        public static extern void RemoveWeatherEffectBJ(weathereffect whichWeatherEffect);
        [NativeLuaMemberAttribute]
        public static extern terraindeformation TerrainDeformationCraterBJ(float duration, bool permanent, location where, float radius, float depth);
        [NativeLuaMemberAttribute]
        public static extern terraindeformation TerrainDeformationRippleBJ(float duration, bool limitNeg, location where, float startRadius, float endRadius, float depth, float wavePeriod, float waveWidth);
        [NativeLuaMemberAttribute]
        public static extern terraindeformation TerrainDeformationWaveBJ(float duration, location source, location target, float radius, float depth, float trailDelay);
        [NativeLuaMemberAttribute]
        public static extern terraindeformation TerrainDeformationRandomBJ(float duration, location where, float radius, float minDelta, float maxDelta, float updateInterval);
        [NativeLuaMemberAttribute]
        public static extern void TerrainDeformationStopBJ(terraindeformation deformation, float duration);
        [NativeLuaMemberAttribute]
        public static extern terraindeformation GetLastCreatedTerrainDeformation();
        [NativeLuaMemberAttribute]
        public static extern lightning AddLightningLoc(string codeName, location where1, location where2);
        [NativeLuaMemberAttribute]
        public static extern bool DestroyLightningBJ(lightning whichBolt);
        [NativeLuaMemberAttribute]
        public static extern bool MoveLightningLoc(lightning whichBolt, location where1, location where2);
        [NativeLuaMemberAttribute]
        public static extern float GetLightningColorABJ(lightning whichBolt);
        [NativeLuaMemberAttribute]
        public static extern float GetLightningColorRBJ(lightning whichBolt);
        [NativeLuaMemberAttribute]
        public static extern float GetLightningColorGBJ(lightning whichBolt);
        [NativeLuaMemberAttribute]
        public static extern float GetLightningColorBBJ(lightning whichBolt);
        [NativeLuaMemberAttribute]
        public static extern bool SetLightningColorBJ(lightning whichBolt, float r, float g, float b, float a);
        [NativeLuaMemberAttribute]
        public static extern lightning GetLastCreatedLightningBJ();
        [NativeLuaMemberAttribute]
        public static extern string GetAbilityEffectBJ(int abilcode, effecttype t, int index);
        [NativeLuaMemberAttribute]
        public static extern string GetAbilitySoundBJ(int abilcode, soundtype t);
        [NativeLuaMemberAttribute]
        public static extern int GetTerrainCliffLevelBJ(location where);
        [NativeLuaMemberAttribute]
        public static extern int GetTerrainTypeBJ(location where);
        [NativeLuaMemberAttribute]
        public static extern int GetTerrainVarianceBJ(location where);
        [NativeLuaMemberAttribute]
        public static extern void SetTerrainTypeBJ(location where, int terrainType, int variation, int area, int shape);
        [NativeLuaMemberAttribute]
        public static extern bool IsTerrainPathableBJ(location where, pathingtype t);
        [NativeLuaMemberAttribute]
        public static extern void SetTerrainPathableBJ(location where, pathingtype t, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetWaterBaseColorBJ(float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern fogmodifier CreateFogModifierRectSimple(player whichPlayer, fogstate whichFogState, rect r, bool afterUnits);
        [NativeLuaMemberAttribute]
        public static extern fogmodifier CreateFogModifierRadiusLocSimple(player whichPlayer, fogstate whichFogState, location center, float radius, bool afterUnits);
        [NativeLuaMemberAttribute]
        public static extern fogmodifier CreateFogModifierRectBJ(bool enabled, player whichPlayer, fogstate whichFogState, rect r);
        [NativeLuaMemberAttribute]
        public static extern fogmodifier CreateFogModifierRadiusLocBJ(bool enabled, player whichPlayer, fogstate whichFogState, location center, float radius);
        [NativeLuaMemberAttribute]
        public static extern fogmodifier GetLastCreatedFogModifier();
        [NativeLuaMemberAttribute]
        public static extern void FogEnableOn();
        [NativeLuaMemberAttribute]
        public static extern void FogEnableOff();
        [NativeLuaMemberAttribute]
        public static extern void FogMaskEnableOn();
        [NativeLuaMemberAttribute]
        public static extern void FogMaskEnableOff();
        [NativeLuaMemberAttribute]
        public static extern void UseTimeOfDayBJ(bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetTerrainFogExBJ(int style, float zstart, float zend, float density, float red, float green, float blue);
        [NativeLuaMemberAttribute]
        public static extern void ResetTerrainFogBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetDoodadAnimationBJ(string animName, int doodadID, float radius, location center);
        [NativeLuaMemberAttribute]
        public static extern void SetDoodadAnimationRectBJ(string animName, int doodadID, rect r);
        [NativeLuaMemberAttribute]
        public static extern void AddUnitAnimationPropertiesBJ(bool add, string animProperties, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern image CreateImageBJ(string file, float size, location where, float zOffset, int imageType);
        [NativeLuaMemberAttribute]
        public static extern void ShowImageBJ(bool flag, image whichImage);
        [NativeLuaMemberAttribute]
        public static extern void SetImagePositionBJ(image whichImage, location where, float zOffset);
        [NativeLuaMemberAttribute]
        public static extern void SetImageColorBJ(image whichImage, float red, float green, float blue, float alpha);
        [NativeLuaMemberAttribute]
        public static extern image GetLastCreatedImage();
        [NativeLuaMemberAttribute]
        public static extern ubersplat CreateUbersplatBJ(location where, string name, float red, float green, float blue, float alpha, bool forcePaused, bool noBirthTime);
        [NativeLuaMemberAttribute]
        public static extern void ShowUbersplatBJ(bool flag, ubersplat whichSplat);
        [NativeLuaMemberAttribute]
        public static extern ubersplat GetLastCreatedUbersplat();
        [NativeLuaMemberAttribute]
        public static extern void PlaySoundBJ(sound soundHandle);
        [NativeLuaMemberAttribute]
        public static extern void StopSoundBJ(sound soundHandle, bool fadeOut);
        [NativeLuaMemberAttribute]
        public static extern void SetSoundVolumeBJ(sound soundHandle, float volumePercent);
        [NativeLuaMemberAttribute]
        public static extern void SetSoundOffsetBJ(float newOffset, sound soundHandle);
        [NativeLuaMemberAttribute]
        public static extern void SetSoundDistanceCutoffBJ(sound soundHandle, float cutoff);
        [NativeLuaMemberAttribute]
        public static extern void SetSoundPitchBJ(sound soundHandle, float pitch);
        [NativeLuaMemberAttribute]
        public static extern void SetSoundPositionLocBJ(sound soundHandle, location loc, float z);
        [NativeLuaMemberAttribute]
        public static extern void AttachSoundToUnitBJ(sound soundHandle, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetSoundConeAnglesBJ(sound soundHandle, float inside, float outside, float outsideVolumePercent);
        [NativeLuaMemberAttribute]
        public static extern void KillSoundWhenDoneBJ(sound soundHandle);
        [NativeLuaMemberAttribute]
        public static extern void PlaySoundAtPointBJ(sound soundHandle, float volumePercent, location loc, float z);
        [NativeLuaMemberAttribute]
        public static extern void PlaySoundOnUnitBJ(sound soundHandle, float volumePercent, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void PlaySoundFromOffsetBJ(sound soundHandle, float volumePercent, float startingOffset);
        [NativeLuaMemberAttribute]
        public static extern void PlayMusicBJ(string musicFileName);
        [NativeLuaMemberAttribute]
        public static extern void PlayMusicExBJ(string musicFileName, float startingOffset, float fadeInTime);
        [NativeLuaMemberAttribute]
        public static extern void SetMusicOffsetBJ(float newOffset);
        [NativeLuaMemberAttribute]
        public static extern void PlayThematicMusicBJ(string musicName);
        [NativeLuaMemberAttribute]
        public static extern void PlayThematicMusicExBJ(string musicName, float startingOffset);
        [NativeLuaMemberAttribute]
        public static extern void SetThematicMusicOffsetBJ(float newOffset);
        [NativeLuaMemberAttribute]
        public static extern void EndThematicMusicBJ();
        [NativeLuaMemberAttribute]
        public static extern void StopMusicBJ(bool fadeOut);
        [NativeLuaMemberAttribute]
        public static extern void ResumeMusicBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetMusicVolumeBJ(float volumePercent);
        [NativeLuaMemberAttribute]
        public static extern float GetSoundDurationBJ(sound soundHandle);
        [NativeLuaMemberAttribute]
        public static extern float GetSoundFileDurationBJ(string musicFileName);
        [NativeLuaMemberAttribute]
        public static extern sound GetLastPlayedSound();
        [NativeLuaMemberAttribute]
        public static extern string GetLastPlayedMusic();
        [NativeLuaMemberAttribute]
        public static extern void VolumeGroupSetVolumeBJ(volumegroup vgroup, float percent);
        [NativeLuaMemberAttribute]
        public static extern void SetCineModeVolumeGroupsImmediateBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetCineModeVolumeGroupsBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetSpeechVolumeGroupsImmediateBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetSpeechVolumeGroupsBJ();
        [NativeLuaMemberAttribute]
        public static extern void VolumeGroupResetImmediateBJ();
        [NativeLuaMemberAttribute]
        public static extern void VolumeGroupResetBJ();
        [NativeLuaMemberAttribute]
        public static extern bool GetSoundIsPlayingBJ(sound soundHandle);
        [NativeLuaMemberAttribute]
        public static extern void WaitForSoundBJ(sound soundHandle, float offset);
        [NativeLuaMemberAttribute]
        public static extern void SetMapMusicIndexedBJ(string musicName, int index);
        [NativeLuaMemberAttribute]
        public static extern void SetMapMusicRandomBJ(string musicName);
        [NativeLuaMemberAttribute]
        public static extern void ClearMapMusicBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetStackedSoundBJ(bool add, sound soundHandle, rect r);
        [NativeLuaMemberAttribute]
        public static extern void StartSoundForPlayerBJ(player whichPlayer, sound soundHandle);
        [NativeLuaMemberAttribute]
        public static extern void VolumeGroupSetVolumeForPlayerBJ(player whichPlayer, volumegroup vgroup, float scale);
        [NativeLuaMemberAttribute]
        public static extern void EnableDawnDusk(bool flag);
        [NativeLuaMemberAttribute]
        public static extern bool IsDawnDuskEnabled();
        [NativeLuaMemberAttribute]
        public static extern void SetAmbientDaySound(string inLabel);
        [NativeLuaMemberAttribute]
        public static extern void SetAmbientNightSound(string inLabel);
        [NativeLuaMemberAttribute]
        public static extern effect AddSpecialEffectLocBJ(location where, string modelName);
        [NativeLuaMemberAttribute]
        public static extern effect AddSpecialEffectTargetUnitBJ(string attachPointName, widget targetWidget, string modelName);
        [NativeLuaMemberAttribute]
        public static extern void DestroyEffectBJ(effect whichEffect);
        [NativeLuaMemberAttribute]
        public static extern effect GetLastCreatedEffectBJ();
        [NativeLuaMemberAttribute]
        public static extern location GetItemLoc(item whichItem);
        [NativeLuaMemberAttribute]
        public static extern float GetItemLifeBJ(widget whichWidget);
        [NativeLuaMemberAttribute]
        public static extern void SetItemLifeBJ(widget whichWidget, float life);
        [NativeLuaMemberAttribute]
        public static extern void AddHeroXPSwapped(int xpToAdd, unit whichHero, bool showEyeCandy);
        [NativeLuaMemberAttribute]
        public static extern void SetHeroLevelBJ(unit whichHero, int newLevel, bool showEyeCandy);
        [NativeLuaMemberAttribute]
        public static extern int DecUnitAbilityLevelSwapped(int abilcode, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern int IncUnitAbilityLevelSwapped(int abilcode, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern int SetUnitAbilityLevelSwapped(int abilcode, unit whichUnit, int level);
        [NativeLuaMemberAttribute]
        public static extern int GetUnitAbilityLevelSwapped(int abilcode, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitHasBuffBJ(unit whichUnit, int buffcode);
        [NativeLuaMemberAttribute]
        public static extern bool UnitRemoveBuffBJ(int buffcode, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitAddItemSwapped(item whichItem, unit whichHero);
        [NativeLuaMemberAttribute]
        public static extern item UnitAddItemByIdSwapped(int itemId, unit whichHero);
        [NativeLuaMemberAttribute]
        public static extern void UnitRemoveItemSwapped(item whichItem, unit whichHero);
        [NativeLuaMemberAttribute]
        public static extern item UnitRemoveItemFromSlotSwapped(int itemSlot, unit whichHero);
        [NativeLuaMemberAttribute]
        public static extern item CreateItemLoc(int itemId, location loc);
        [NativeLuaMemberAttribute]
        public static extern item GetLastCreatedItem();
        [NativeLuaMemberAttribute]
        public static extern item GetLastRemovedItem();
        [NativeLuaMemberAttribute]
        public static extern void SetItemPositionLoc(item whichItem, location loc);
        [NativeLuaMemberAttribute]
        public static extern int GetLearnedSkillBJ();
        [NativeLuaMemberAttribute]
        public static extern void SuspendHeroXPBJ(bool flag, unit whichHero);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerHandicapXPBJ(player whichPlayer, float handicapPercent);
        [NativeLuaMemberAttribute]
        public static extern float GetPlayerHandicapXPBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerHandicapBJ(player whichPlayer, float handicapPercent);
        [NativeLuaMemberAttribute]
        public static extern float GetPlayerHandicapBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern int GetHeroStatBJ(int whichStat, unit whichHero, bool includeBonuses);
        [NativeLuaMemberAttribute]
        public static extern void SetHeroStat(unit whichHero, int whichStat, int value);
        [NativeLuaMemberAttribute]
        public static extern void ModifyHeroStat(int whichStat, unit whichHero, int modifyMethod, int value);
        [NativeLuaMemberAttribute]
        public static extern bool ModifyHeroSkillPoints(unit whichHero, int modifyMethod, int value);
        [NativeLuaMemberAttribute]
        public static extern bool UnitDropItemPointBJ(unit whichUnit, item whichItem, float x, float y);
        [NativeLuaMemberAttribute]
        public static extern bool UnitDropItemPointLoc(unit whichUnit, item whichItem, location loc);
        [NativeLuaMemberAttribute]
        public static extern bool UnitDropItemSlotBJ(unit whichUnit, item whichItem, int slot);
        [NativeLuaMemberAttribute]
        public static extern bool UnitDropItemTargetBJ(unit whichUnit, item whichItem, widget target);
        [NativeLuaMemberAttribute]
        public static extern bool UnitUseItemDestructable(unit whichUnit, item whichItem, widget target);
        [NativeLuaMemberAttribute]
        public static extern bool UnitUseItemPointLoc(unit whichUnit, item whichItem, location loc);
        [NativeLuaMemberAttribute]
        public static extern item UnitItemInSlotBJ(unit whichUnit, int itemSlot);
        [NativeLuaMemberAttribute]
        public static extern int GetInventoryIndexOfItemTypeBJ(unit whichUnit, int itemId);
        [NativeLuaMemberAttribute]
        public static extern item GetItemOfTypeFromUnitBJ(unit whichUnit, int itemId);
        [NativeLuaMemberAttribute]
        public static extern bool UnitHasItemOfTypeBJ(unit whichUnit, int itemId);
        [NativeLuaMemberAttribute]
        public static extern int UnitInventoryCount(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern int UnitInventorySizeBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetItemInvulnerableBJ(item whichItem, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetItemDropOnDeathBJ(item whichItem, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetItemDroppableBJ(item whichItem, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetItemPlayerBJ(item whichItem, player whichPlayer, bool changeColor);
        [NativeLuaMemberAttribute]
        public static extern void SetItemVisibleBJ(bool show, item whichItem);
        [NativeLuaMemberAttribute]
        public static extern bool IsItemHiddenBJ(item whichItem);
        [NativeLuaMemberAttribute]
        public static extern int ChooseRandomItemBJ(int level);
        [NativeLuaMemberAttribute]
        public static extern int ChooseRandomItemExBJ(int level, itemtype whichType);
        [NativeLuaMemberAttribute]
        public static extern int ChooseRandomNPBuildingBJ();
        [NativeLuaMemberAttribute]
        public static extern int ChooseRandomCreepBJ(int level);
        [NativeLuaMemberAttribute]
        public static extern void EnumItemsInRectBJ(rect r, System.Action actionFunc);
        [NativeLuaMemberAttribute]
        public static extern void RandomItemInRectBJEnum();
        [NativeLuaMemberAttribute]
        public static extern item RandomItemInRectBJ(rect r, boolexpr filter);
        [NativeLuaMemberAttribute]
        public static extern item RandomItemInRectSimpleBJ(rect r);
        [NativeLuaMemberAttribute]
        public static extern bool CheckItemStatus(item whichItem, int status);
        [NativeLuaMemberAttribute]
        public static extern bool CheckItemcodeStatus(int itemId, int status);
        [NativeLuaMemberAttribute]
        public static extern int UnitId2OrderIdBJ(int unitId);
        [NativeLuaMemberAttribute]
        public static extern int String2UnitIdBJ(string unitIdString);
        [NativeLuaMemberAttribute]
        public static extern string UnitId2StringBJ(int unitId);
        [NativeLuaMemberAttribute]
        public static extern int String2OrderIdBJ(string orderIdString);
        [NativeLuaMemberAttribute]
        public static extern string OrderId2StringBJ(int orderId);
        [NativeLuaMemberAttribute]
        public static extern int GetIssuedOrderIdBJ();
        [NativeLuaMemberAttribute]
        public static extern unit GetKillingUnitBJ();
        [NativeLuaMemberAttribute]
        public static extern unit CreateUnitAtLocSaveLast(player id, int unitid, location loc, float face);
        [NativeLuaMemberAttribute]
        public static extern unit GetLastCreatedUnit();
        [NativeLuaMemberAttribute]
        public static extern group CreateNUnitsAtLoc(int count, int unitId, player whichPlayer, location loc, float face);
        [NativeLuaMemberAttribute]
        public static extern group CreateNUnitsAtLocFacingLocBJ(int count, int unitId, player whichPlayer, location loc, location lookAt);
        [NativeLuaMemberAttribute]
        public static extern void GetLastCreatedGroupEnum();
        [NativeLuaMemberAttribute]
        public static extern group GetLastCreatedGroup();
        [NativeLuaMemberAttribute]
        public static extern unit CreateCorpseLocBJ(int unitid, player whichPlayer, location loc);
        [NativeLuaMemberAttribute]
        public static extern void UnitSuspendDecayBJ(bool suspend, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void DelayedSuspendDecayStopAnimEnum();
        [NativeLuaMemberAttribute]
        public static extern void DelayedSuspendDecayBoneEnum();
        [NativeLuaMemberAttribute]
        public static extern void DelayedSuspendDecayFleshEnum();
        [NativeLuaMemberAttribute]
        public static extern void DelayedSuspendDecay();
        [NativeLuaMemberAttribute]
        public static extern void DelayedSuspendDecayCreate();
        [NativeLuaMemberAttribute]
        public static extern unit CreatePermanentCorpseLocBJ(int style, int unitid, player whichPlayer, location loc, float facing);
        [NativeLuaMemberAttribute]
        public static extern float GetUnitStateSwap(unitstate whichState, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern float GetUnitStatePercent(unit whichUnit, unitstate whichState, unitstate whichMaxState);
        [NativeLuaMemberAttribute]
        public static extern float GetUnitLifePercent(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern float GetUnitManaPercent(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SelectUnitSingle(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SelectGroupBJEnum();
        [NativeLuaMemberAttribute]
        public static extern void SelectGroupBJ(group g);
        [NativeLuaMemberAttribute]
        public static extern void SelectUnitAdd(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SelectUnitRemove(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void ClearSelectionForPlayer(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SelectUnitForPlayerSingle(unit whichUnit, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SelectGroupForPlayerBJ(group g, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SelectUnitAddForPlayer(unit whichUnit, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SelectUnitRemoveForPlayer(unit whichUnit, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitLifeBJ(unit whichUnit, float newValue);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitManaBJ(unit whichUnit, float newValue);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitLifePercentBJ(unit whichUnit, float percent);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitManaPercentBJ(unit whichUnit, float percent);
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitDeadBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitAliveBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void IsUnitGroupDeadBJEnum();
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitGroupDeadBJ(group g);
        [NativeLuaMemberAttribute]
        public static extern void IsUnitGroupEmptyBJEnum();
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitGroupEmptyBJ(group g);
        [NativeLuaMemberAttribute]
        public static extern void IsUnitGroupInRectBJEnum();
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitGroupInRectBJ(group g, rect r);
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitHiddenBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void ShowUnitHide(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void ShowUnitShow(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool IssueHauntOrderAtLocBJFilter();
        [NativeLuaMemberAttribute]
        public static extern bool IssueHauntOrderAtLocBJ(unit whichPeon, location loc);
        [NativeLuaMemberAttribute]
        public static extern bool IssueBuildOrderByIdLocBJ(unit whichPeon, int unitId, location loc);
        [NativeLuaMemberAttribute]
        public static extern bool IssueTrainOrderByIdBJ(unit whichUnit, int unitId);
        [NativeLuaMemberAttribute]
        public static extern bool GroupTrainOrderByIdBJ(group g, int unitId);
        [NativeLuaMemberAttribute]
        public static extern bool IssueUpgradeOrderByIdBJ(unit whichUnit, int techId);
        [NativeLuaMemberAttribute]
        public static extern unit GetAttackedUnitBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetUnitFlyHeightBJ(unit whichUnit, float newHeight, float rate);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitTurnSpeedBJ(unit whichUnit, float turnSpeed);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitPropWindowBJ(unit whichUnit, float propWindow);
        [NativeLuaMemberAttribute]
        public static extern float GetUnitPropWindowBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern float GetUnitDefaultPropWindowBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitBlendTimeBJ(unit whichUnit, float blendTime);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitAcquireRangeBJ(unit whichUnit, float acquireRange);
        [NativeLuaMemberAttribute]
        public static extern void UnitSetCanSleepBJ(unit whichUnit, bool canSleep);
        [NativeLuaMemberAttribute]
        public static extern bool UnitCanSleepBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void UnitWakeUpBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitIsSleepingBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void WakePlayerUnitsEnum();
        [NativeLuaMemberAttribute]
        public static extern void WakePlayerUnits(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void EnableCreepSleepBJ(bool enable);
        [NativeLuaMemberAttribute]
        public static extern bool UnitGenerateAlarms(unit whichUnit, bool generate);
        [NativeLuaMemberAttribute]
        public static extern bool DoesUnitGenerateAlarms(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void PauseAllUnitsBJEnum();
        [NativeLuaMemberAttribute]
        public static extern void PauseAllUnitsBJ(bool pause);
        [NativeLuaMemberAttribute]
        public static extern void PauseUnitBJ(bool pause, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitPausedBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void UnitPauseTimedLifeBJ(bool flag, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void UnitApplyTimedLifeBJ(float duration, int buffId, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void UnitShareVisionBJ(bool share, unit whichUnit, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void UnitRemoveBuffsBJ(int buffType, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void UnitRemoveBuffsExBJ(int polarity, int resist, unit whichUnit, bool bTLife, bool bAura);
        [NativeLuaMemberAttribute]
        public static extern int UnitCountBuffsExBJ(int polarity, int resist, unit whichUnit, bool bTLife, bool bAura);
        [NativeLuaMemberAttribute]
        public static extern bool UnitRemoveAbilityBJ(int abilityId, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitAddAbilityBJ(int abilityId, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitRemoveTypeBJ(unittype whichType, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitAddTypeBJ(unittype whichType, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitMakeAbilityPermanentBJ(bool permanent, int abilityId, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitExplodedBJ(unit whichUnit, bool exploded);
        [NativeLuaMemberAttribute]
        public static extern void ExplodeUnitBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern unit GetTransportUnitBJ();
        [NativeLuaMemberAttribute]
        public static extern unit GetLoadedUnitBJ();
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitInTransportBJ(unit whichUnit, unit whichTransport);
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitLoadedBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool IsUnitIllusionBJ(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern unit ReplaceUnitBJ(unit whichUnit, int newUnitId, int unitStateMethod);
        [NativeLuaMemberAttribute]
        public static extern unit GetLastReplacedUnitBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetUnitPositionLocFacingBJ(unit whichUnit, location loc, float facing);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitPositionLocFacingLocBJ(unit whichUnit, location loc, location lookAt);
        [NativeLuaMemberAttribute]
        public static extern void AddItemToStockBJ(int itemId, unit whichUnit, int currentStock, int stockMax);
        [NativeLuaMemberAttribute]
        public static extern void AddUnitToStockBJ(int unitId, unit whichUnit, int currentStock, int stockMax);
        [NativeLuaMemberAttribute]
        public static extern void RemoveItemFromStockBJ(int itemId, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void RemoveUnitFromStockBJ(int unitId, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitUseFoodBJ(bool enable, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern bool UnitDamagePointLoc(unit whichUnit, float delay, float radius, location loc, float amount, attacktype whichAttack, damagetype whichDamage);
        [NativeLuaMemberAttribute]
        public static extern bool UnitDamageTargetBJ(unit whichUnit, unit target, float amount, attacktype whichAttack, damagetype whichDamage);
        [NativeLuaMemberAttribute]
        public static extern destructable CreateDestructableLoc(int objectid, location loc, float facing, float scale, int variation);
        [NativeLuaMemberAttribute]
        public static extern destructable CreateDeadDestructableLocBJ(int objectid, location loc, float facing, float scale, int variation);
        [NativeLuaMemberAttribute]
        public static extern destructable GetLastCreatedDestructable();
        [NativeLuaMemberAttribute]
        public static extern void ShowDestructableBJ(bool flag, destructable d);
        [NativeLuaMemberAttribute]
        public static extern void SetDestructableInvulnerableBJ(destructable d, bool flag);
        [NativeLuaMemberAttribute]
        public static extern bool IsDestructableInvulnerableBJ(destructable d);
        [NativeLuaMemberAttribute]
        public static extern location GetDestructableLoc(destructable whichDestructable);
        [NativeLuaMemberAttribute]
        public static extern void EnumDestructablesInRectAll(rect r, System.Action actionFunc);
        [NativeLuaMemberAttribute]
        public static extern bool EnumDestructablesInCircleBJFilter();
        [NativeLuaMemberAttribute]
        public static extern bool IsDestructableDeadBJ(destructable d);
        [NativeLuaMemberAttribute]
        public static extern bool IsDestructableAliveBJ(destructable d);
        [NativeLuaMemberAttribute]
        public static extern void RandomDestructableInRectBJEnum();
        [NativeLuaMemberAttribute]
        public static extern destructable RandomDestructableInRectBJ(rect r, boolexpr filter);
        [NativeLuaMemberAttribute]
        public static extern destructable RandomDestructableInRectSimpleBJ(rect r);
        [NativeLuaMemberAttribute]
        public static extern void EnumDestructablesInCircleBJ(float radius, location loc, System.Action actionFunc);
        [NativeLuaMemberAttribute]
        public static extern void SetDestructableLifePercentBJ(destructable d, float percent);
        [NativeLuaMemberAttribute]
        public static extern void SetDestructableMaxLifeBJ(destructable d, float max);
        [NativeLuaMemberAttribute]
        public static extern void ModifyGateBJ(int gateOperation, destructable d);
        [NativeLuaMemberAttribute]
        public static extern int GetElevatorHeight(destructable d);
        [NativeLuaMemberAttribute]
        public static extern void ChangeElevatorHeight(destructable d, int newHeight);
        [NativeLuaMemberAttribute]
        public static extern void NudgeUnitsInRectEnum();
        [NativeLuaMemberAttribute]
        public static extern void NudgeItemsInRectEnum();
        [NativeLuaMemberAttribute]
        public static extern void NudgeObjectsInRect(rect nudgeArea);
        [NativeLuaMemberAttribute]
        public static extern void NearbyElevatorExistsEnum();
        [NativeLuaMemberAttribute]
        public static extern bool NearbyElevatorExists(float x, float y);
        [NativeLuaMemberAttribute]
        public static extern void FindElevatorWallBlockerEnum();
        [NativeLuaMemberAttribute]
        public static extern void ChangeElevatorWallBlocker(float x, float y, float facing, bool open);
        [NativeLuaMemberAttribute]
        public static extern void ChangeElevatorWalls(bool open, int walls, destructable d);
        [NativeLuaMemberAttribute]
        public static extern void WaygateActivateBJ(bool activate, unit waygate);
        [NativeLuaMemberAttribute]
        public static extern bool WaygateIsActiveBJ(unit waygate);
        [NativeLuaMemberAttribute]
        public static extern void WaygateSetDestinationLocBJ(unit waygate, location loc);
        [NativeLuaMemberAttribute]
        public static extern location WaygateGetDestinationLocBJ(unit waygate);
        [NativeLuaMemberAttribute]
        public static extern void UnitSetUsesAltIconBJ(bool flag, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void ForceUIKeyBJ(player whichPlayer, string key);
        [NativeLuaMemberAttribute]
        public static extern void ForceUICancelBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void ForGroupBJ(group whichGroup, System.Action callback);
        [NativeLuaMemberAttribute]
        public static extern void GroupAddUnitSimple(unit whichUnit, group whichGroup);
        [NativeLuaMemberAttribute]
        public static extern void GroupRemoveUnitSimple(unit whichUnit, group whichGroup);
        [NativeLuaMemberAttribute]
        public static extern void GroupAddGroupEnum();
        [NativeLuaMemberAttribute]
        public static extern void GroupAddGroup(group sourceGroup, group destGroup);
        [NativeLuaMemberAttribute]
        public static extern void GroupRemoveGroupEnum();
        [NativeLuaMemberAttribute]
        public static extern void GroupRemoveGroup(group sourceGroup, group destGroup);
        [NativeLuaMemberAttribute]
        public static extern void ForceAddPlayerSimple(player whichPlayer, force whichForce);
        [NativeLuaMemberAttribute]
        public static extern void ForceRemovePlayerSimple(player whichPlayer, force whichForce);
        [NativeLuaMemberAttribute]
        public static extern void GroupPickRandomUnitEnum();
        [NativeLuaMemberAttribute]
        public static extern unit GroupPickRandomUnit(group whichGroup);
        [NativeLuaMemberAttribute]
        public static extern void ForcePickRandomPlayerEnum();
        [NativeLuaMemberAttribute]
        public static extern player ForcePickRandomPlayer(force whichForce);
        [NativeLuaMemberAttribute]
        public static extern void EnumUnitsSelected(player whichPlayer, boolexpr enumFilter, System.Action enumAction);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsInRectMatching(rect r, boolexpr filter);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsInRectAll(rect r);
        [NativeLuaMemberAttribute]
        public static extern bool GetUnitsInRectOfPlayerFilter();
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsInRectOfPlayer(rect r, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsInRangeOfLocMatching(float radius, location whichLocation, boolexpr filter);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsInRangeOfLocAll(float radius, location whichLocation);
        [NativeLuaMemberAttribute]
        public static extern bool GetUnitsOfTypeIdAllFilter();
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsOfTypeIdAll(int unitid);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsOfPlayerMatching(player whichPlayer, boolexpr filter);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsOfPlayerAll(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern bool GetUnitsOfPlayerAndTypeIdFilter();
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsOfPlayerAndTypeId(player whichPlayer, int unitid);
        [NativeLuaMemberAttribute]
        public static extern group GetUnitsSelectedAll(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern force GetForceOfPlayer(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern force GetPlayersAll();
        [NativeLuaMemberAttribute]
        public static extern force GetPlayersByMapControl(mapcontrol whichControl);
        [NativeLuaMemberAttribute]
        public static extern force GetPlayersAllies(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern force GetPlayersEnemies(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern force GetPlayersMatching(boolexpr filter);
        [NativeLuaMemberAttribute]
        public static extern void CountUnitsInGroupEnum();
        [NativeLuaMemberAttribute]
        public static extern int CountUnitsInGroup(group g);
        [NativeLuaMemberAttribute]
        public static extern void CountPlayersInForceEnum();
        [NativeLuaMemberAttribute]
        public static extern int CountPlayersInForceBJ(force f);
        [NativeLuaMemberAttribute]
        public static extern void GetRandomSubGroupEnum();
        [NativeLuaMemberAttribute]
        public static extern group GetRandomSubGroup(int count, group sourceGroup);
        [NativeLuaMemberAttribute]
        public static extern bool LivingPlayerUnitsOfTypeIdFilter();
        [NativeLuaMemberAttribute]
        public static extern int CountLivingPlayerUnitsOfTypeId(int unitId, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void ResetUnitAnimation(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitTimeScalePercent(unit whichUnit, float percentScale);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitScalePercent(unit whichUnit, float percentScaleX, float percentScaleY, float percentScaleZ);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitVertexColorBJ(unit whichUnit, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void UnitAddIndicatorBJ(unit whichUnit, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void DestructableAddIndicatorBJ(destructable whichDestructable, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void ItemAddIndicatorBJ(item whichItem, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitFacingToFaceLocTimed(unit whichUnit, location target, float duration);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitFacingToFaceUnitTimed(unit whichUnit, unit target, float duration);
        [NativeLuaMemberAttribute]
        public static extern void QueueUnitAnimationBJ(unit whichUnit, string whichAnimation);
        [NativeLuaMemberAttribute]
        public static extern void SetDestructableAnimationBJ(destructable d, string whichAnimation);
        [NativeLuaMemberAttribute]
        public static extern void QueueDestructableAnimationBJ(destructable d, string whichAnimation);
        [NativeLuaMemberAttribute]
        public static extern void SetDestAnimationSpeedPercent(destructable d, float percentScale);
        [NativeLuaMemberAttribute]
        public static extern void DialogDisplayBJ(bool flag, dialog whichDialog, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void DialogSetMessageBJ(dialog whichDialog, string message);
        [NativeLuaMemberAttribute]
        public static extern button DialogAddButtonBJ(dialog whichDialog, string buttonText);
        [NativeLuaMemberAttribute]
        public static extern button DialogAddButtonWithHotkeyBJ(dialog whichDialog, string buttonText, int hotkey);
        [NativeLuaMemberAttribute]
        public static extern void DialogClearBJ(dialog whichDialog);
        [NativeLuaMemberAttribute]
        public static extern button GetLastCreatedButtonBJ();
        [NativeLuaMemberAttribute]
        public static extern button GetClickedButtonBJ();
        [NativeLuaMemberAttribute]
        public static extern dialog GetClickedDialogBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAllianceBJ(player sourcePlayer, alliancetype whichAllianceSetting, bool value, player otherPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAllianceStateAllyBJ(player sourcePlayer, player otherPlayer, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAllianceStateVisionBJ(player sourcePlayer, player otherPlayer, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAllianceStateControlBJ(player sourcePlayer, player otherPlayer, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAllianceStateFullControlBJ(player sourcePlayer, player otherPlayer, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAllianceStateBJ(player sourcePlayer, player otherPlayer, int allianceState);
        [NativeLuaMemberAttribute]
        public static extern void SetForceAllianceStateBJ(force sourceForce, force targetForce, int allianceState);
        [NativeLuaMemberAttribute]
        public static extern bool PlayersAreCoAllied(player playerA, player playerB);
        [NativeLuaMemberAttribute]
        public static extern void ShareEverythingWithTeamAI(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void ShareEverythingWithTeam(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void ConfigureNeutralVictim();
        [NativeLuaMemberAttribute]
        public static extern void MakeUnitsPassiveForPlayerEnum();
        [NativeLuaMemberAttribute]
        public static extern void MakeUnitsPassiveForPlayer(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MakeUnitsPassiveForTeam(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern bool AllowVictoryDefeat(playergameresult gameResult);
        [NativeLuaMemberAttribute]
        public static extern void EndGameBJ();
        [NativeLuaMemberAttribute]
        public static extern void MeleeVictoryDialogBJ(player whichPlayer, bool leftGame);
        [NativeLuaMemberAttribute]
        public static extern void MeleeDefeatDialogBJ(player whichPlayer, bool leftGame);
        [NativeLuaMemberAttribute]
        public static extern void GameOverDialogBJ(player whichPlayer, bool leftGame);
        [NativeLuaMemberAttribute]
        public static extern void RemovePlayerPreserveUnitsBJ(player whichPlayer, playergameresult gameResult, bool leftGame);
        [NativeLuaMemberAttribute]
        public static extern void CustomVictoryOkBJ();
        [NativeLuaMemberAttribute]
        public static extern void CustomVictoryQuitBJ();
        [NativeLuaMemberAttribute]
        public static extern void CustomVictoryDialogBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void CustomVictorySkipBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void CustomVictoryBJ(player whichPlayer, bool showDialog, bool showScores);
        [NativeLuaMemberAttribute]
        public static extern void CustomDefeatRestartBJ();
        [NativeLuaMemberAttribute]
        public static extern void CustomDefeatReduceDifficultyBJ();
        [NativeLuaMemberAttribute]
        public static extern void CustomDefeatLoadBJ();
        [NativeLuaMemberAttribute]
        public static extern void CustomDefeatQuitBJ();
        [NativeLuaMemberAttribute]
        public static extern void CustomDefeatDialogBJ(player whichPlayer, string message);
        [NativeLuaMemberAttribute]
        public static extern void CustomDefeatBJ(player whichPlayer, string message);
        [NativeLuaMemberAttribute]
        public static extern void SetNextLevelBJ(string nextLevel);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerOnScoreScreenBJ(bool flag, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern quest CreateQuestBJ(int questType, string title, string description, string iconPath);
        [NativeLuaMemberAttribute]
        public static extern void DestroyQuestBJ(quest whichQuest);
        [NativeLuaMemberAttribute]
        public static extern void QuestSetEnabledBJ(bool enabled, quest whichQuest);
        [NativeLuaMemberAttribute]
        public static extern void QuestSetTitleBJ(quest whichQuest, string title);
        [NativeLuaMemberAttribute]
        public static extern void QuestSetDescriptionBJ(quest whichQuest, string description);
        [NativeLuaMemberAttribute]
        public static extern void QuestSetCompletedBJ(quest whichQuest, bool completed);
        [NativeLuaMemberAttribute]
        public static extern void QuestSetFailedBJ(quest whichQuest, bool failed);
        [NativeLuaMemberAttribute]
        public static extern void QuestSetDiscoveredBJ(quest whichQuest, bool discovered);
        [NativeLuaMemberAttribute]
        public static extern quest GetLastCreatedQuestBJ();
        [NativeLuaMemberAttribute]
        public static extern questitem CreateQuestItemBJ(quest whichQuest, string description);
        [NativeLuaMemberAttribute]
        public static extern void QuestItemSetDescriptionBJ(questitem whichQuestItem, string description);
        [NativeLuaMemberAttribute]
        public static extern void QuestItemSetCompletedBJ(questitem whichQuestItem, bool completed);
        [NativeLuaMemberAttribute]
        public static extern questitem GetLastCreatedQuestItemBJ();
        [NativeLuaMemberAttribute]
        public static extern defeatcondition CreateDefeatConditionBJ(string description);
        [NativeLuaMemberAttribute]
        public static extern void DestroyDefeatConditionBJ(defeatcondition whichCondition);
        [NativeLuaMemberAttribute]
        public static extern void DefeatConditionSetDescriptionBJ(defeatcondition whichCondition, string description);
        [NativeLuaMemberAttribute]
        public static extern defeatcondition GetLastCreatedDefeatConditionBJ();
        [NativeLuaMemberAttribute]
        public static extern void FlashQuestDialogButtonBJ();
        [NativeLuaMemberAttribute]
        public static extern void QuestMessageBJ(force f, int messageType, string message);
        [NativeLuaMemberAttribute]
        public static extern timer StartTimerBJ(timer t, bool periodic, float timeout);
        [NativeLuaMemberAttribute]
        public static extern timer CreateTimerBJ(bool periodic, float timeout);
        [NativeLuaMemberAttribute]
        public static extern void DestroyTimerBJ(timer whichTimer);
        [NativeLuaMemberAttribute]
        public static extern void PauseTimerBJ(bool pause, timer whichTimer);
        [NativeLuaMemberAttribute]
        public static extern timer GetLastCreatedTimerBJ();
        [NativeLuaMemberAttribute]
        public static extern timerdialog CreateTimerDialogBJ(timer t, string title);
        [NativeLuaMemberAttribute]
        public static extern void DestroyTimerDialogBJ(timerdialog td);
        [NativeLuaMemberAttribute]
        public static extern void TimerDialogSetTitleBJ(timerdialog td, string title);
        [NativeLuaMemberAttribute]
        public static extern void TimerDialogSetTitleColorBJ(timerdialog td, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void TimerDialogSetTimeColorBJ(timerdialog td, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void TimerDialogSetSpeedBJ(timerdialog td, float speedMultFactor);
        [NativeLuaMemberAttribute]
        public static extern void TimerDialogDisplayForPlayerBJ(bool show, timerdialog td, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void TimerDialogDisplayBJ(bool show, timerdialog td);
        [NativeLuaMemberAttribute]
        public static extern timerdialog GetLastCreatedTimerDialogBJ();
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardResizeBJ(leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetPlayerItemValueBJ(player whichPlayer, leaderboard lb, int val);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetPlayerItemLabelBJ(player whichPlayer, leaderboard lb, string val);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetPlayerItemStyleBJ(player whichPlayer, leaderboard lb, bool showLabel, bool showValue, bool showIcon);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetPlayerItemLabelColorBJ(player whichPlayer, leaderboard lb, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetPlayerItemValueColorBJ(player whichPlayer, leaderboard lb, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetLabelColorBJ(leaderboard lb, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetValueColorBJ(leaderboard lb, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetLabelBJ(leaderboard lb, string label);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSetStyleBJ(leaderboard lb, bool showLabel, bool showNames, bool showValues, bool showIcons);
        [NativeLuaMemberAttribute]
        public static extern int LeaderboardGetItemCountBJ(leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern bool LeaderboardHasPlayerItemBJ(leaderboard lb, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void ForceSetLeaderboardBJ(leaderboard lb, force toForce);
        [NativeLuaMemberAttribute]
        public static extern leaderboard CreateLeaderboardBJ(force toForce, string label);
        [NativeLuaMemberAttribute]
        public static extern void DestroyLeaderboardBJ(leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardDisplayBJ(bool show, leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardAddItemBJ(player whichPlayer, leaderboard lb, string label, int value);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardRemovePlayerItemBJ(player whichPlayer, leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSortItemsBJ(leaderboard lb, int sortType, bool ascending);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSortItemsByPlayerBJ(leaderboard lb, bool ascending);
        [NativeLuaMemberAttribute]
        public static extern void LeaderboardSortItemsByLabelBJ(leaderboard lb, bool ascending);
        [NativeLuaMemberAttribute]
        public static extern int LeaderboardGetPlayerIndexBJ(player whichPlayer, leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern player LeaderboardGetIndexedPlayerBJ(int position, leaderboard lb);
        [NativeLuaMemberAttribute]
        public static extern leaderboard PlayerGetLeaderboardBJ(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern leaderboard GetLastCreatedLeaderboard();
        [NativeLuaMemberAttribute]
        public static extern multiboard CreateMultiboardBJ(int cols, int rows, string title);
        [NativeLuaMemberAttribute]
        public static extern void DestroyMultiboardBJ(multiboard mb);
        [NativeLuaMemberAttribute]
        public static extern multiboard GetLastCreatedMultiboard();
        [NativeLuaMemberAttribute]
        public static extern void MultiboardDisplayBJ(bool show, multiboard mb);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardMinimizeBJ(bool minimize, multiboard mb);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardSetTitleTextColorBJ(multiboard mb, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardAllowDisplayBJ(bool flag);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardSetItemStyleBJ(multiboard mb, int col, int row, bool showValue, bool showIcon);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardSetItemValueBJ(multiboard mb, int col, int row, string val);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardSetItemColorBJ(multiboard mb, int col, int row, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardSetItemWidthBJ(multiboard mb, int col, int row, float width);
        [NativeLuaMemberAttribute]
        public static extern void MultiboardSetItemIconBJ(multiboard mb, int col, int row, string iconFileName);
        [NativeLuaMemberAttribute]
        public static extern float TextTagSize2Height(float size);
        [NativeLuaMemberAttribute]
        public static extern float TextTagSpeed2Velocity(float speed);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagColorBJ(texttag tt, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagVelocityBJ(texttag tt, float speed, float angle);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagTextBJ(texttag tt, string s, float size);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagPosBJ(texttag tt, location loc, float zOffset);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagPosUnitBJ(texttag tt, unit whichUnit, float zOffset);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagSuspendedBJ(texttag tt, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagPermanentBJ(texttag tt, bool flag);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagAgeBJ(texttag tt, float age);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagLifespanBJ(texttag tt, float lifespan);
        [NativeLuaMemberAttribute]
        public static extern void SetTextTagFadepointBJ(texttag tt, float fadepoint);
        [NativeLuaMemberAttribute]
        public static extern texttag CreateTextTagLocBJ(string s, location loc, float zOffset, float size, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern texttag CreateTextTagUnitBJ(string s, unit whichUnit, float zOffset, float size, float red, float green, float blue, float transparency);
        [NativeLuaMemberAttribute]
        public static extern void DestroyTextTagBJ(texttag tt);
        [NativeLuaMemberAttribute]
        public static extern void ShowTextTagForceBJ(bool show, texttag tt, force whichForce);
        [NativeLuaMemberAttribute]
        public static extern texttag GetLastCreatedTextTag();
        [NativeLuaMemberAttribute]
        public static extern void PauseGameOn();
        [NativeLuaMemberAttribute]
        public static extern void PauseGameOff();
        [NativeLuaMemberAttribute]
        public static extern void SetUserControlForceOn(force whichForce);
        [NativeLuaMemberAttribute]
        public static extern void SetUserControlForceOff(force whichForce);
        [NativeLuaMemberAttribute]
        public static extern void ShowInterfaceForceOn(force whichForce, float fadeDuration);
        [NativeLuaMemberAttribute]
        public static extern void ShowInterfaceForceOff(force whichForce, float fadeDuration);
        [NativeLuaMemberAttribute]
        public static extern void PingMinimapForForce(force whichForce, float x, float y, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PingMinimapLocForForce(force whichForce, location loc, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PingMinimapForPlayer(player whichPlayer, float x, float y, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PingMinimapLocForPlayer(player whichPlayer, location loc, float duration);
        [NativeLuaMemberAttribute]
        public static extern void PingMinimapForForceEx(force whichForce, float x, float y, float duration, int style, float red, float green, float blue);
        [NativeLuaMemberAttribute]
        public static extern void PingMinimapLocForForceEx(force whichForce, location loc, float duration, int style, float red, float green, float blue);
        [NativeLuaMemberAttribute]
        public static extern void EnableWorldFogBoundaryBJ(bool enable, force f);
        [NativeLuaMemberAttribute]
        public static extern void EnableOcclusionBJ(bool enable, force f);
        [NativeLuaMemberAttribute]
        public static extern void CancelCineSceneBJ();
        [NativeLuaMemberAttribute]
        public static extern void TryInitCinematicBehaviorBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetCinematicSceneBJ(sound soundHandle, int portraitUnitId, playercolor color, string speakerTitle, string text, float sceneDuration, float voiceoverDuration);
        [NativeLuaMemberAttribute]
        public static extern float GetTransmissionDuration(sound soundHandle, int timeType, float timeVal);
        [NativeLuaMemberAttribute]
        public static extern void WaitTransmissionDuration(sound soundHandle, int timeType, float timeVal);
        [NativeLuaMemberAttribute]
        public static extern void DoTransmissionBasicsXYBJ(int unitId, playercolor color, float x, float y, sound soundHandle, string unitName, string message, float duration);
        [NativeLuaMemberAttribute]
        public static extern void TransmissionFromUnitWithNameBJ(force toForce, unit whichUnit, string unitName, sound soundHandle, string message, int timeType, float timeVal, bool wait);
        [NativeLuaMemberAttribute]
        public static extern void TransmissionFromUnitTypeWithNameBJ(force toForce, player fromPlayer, int unitId, string unitName, location loc, sound soundHandle, string message, int timeType, float timeVal, bool wait);
        [NativeLuaMemberAttribute]
        public static extern float GetLastTransmissionDurationBJ();
        [NativeLuaMemberAttribute]
        public static extern void ForceCinematicSubtitlesBJ(bool flag);
        [NativeLuaMemberAttribute]
        public static extern void CinematicModeExBJ(bool cineMode, force forForce, float interfaceFadeTime);
        [NativeLuaMemberAttribute]
        public static extern void CinematicModeBJ(bool cineMode, force forForce);
        [NativeLuaMemberAttribute]
        public static extern void DisplayCineFilterBJ(bool flag);
        [NativeLuaMemberAttribute]
        public static extern void CinematicFadeCommonBJ(float red, float green, float blue, float duration, string tex, float startTrans, float endTrans);
        [NativeLuaMemberAttribute]
        public static extern void FinishCinematicFadeBJ();
        [NativeLuaMemberAttribute]
        public static extern void FinishCinematicFadeAfterBJ(float duration);
        [NativeLuaMemberAttribute]
        public static extern void ContinueCinematicFadeBJ();
        [NativeLuaMemberAttribute]
        public static extern void ContinueCinematicFadeAfterBJ(float duration, float red, float green, float blue, float trans, string tex);
        [NativeLuaMemberAttribute]
        public static extern void AbortCinematicFadeBJ();
        [NativeLuaMemberAttribute]
        public static extern void CinematicFadeBJ(int fadetype, float duration, string tex, float red, float green, float blue, float trans);
        [NativeLuaMemberAttribute]
        public static extern void CinematicFilterGenericBJ(float duration, blendmode bmode, string tex, float red0, float green0, float blue0, float trans0, float red1, float green1, float blue1, float trans1);
        [NativeLuaMemberAttribute]
        public static extern void RescueUnitBJ(unit whichUnit, player rescuer, bool changeColor);
        [NativeLuaMemberAttribute]
        public static extern void TriggerActionUnitRescuedBJ();
        [NativeLuaMemberAttribute]
        public static extern void TryInitRescuableTriggersBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetRescueUnitColorChangeBJ(bool changeColor);
        [NativeLuaMemberAttribute]
        public static extern void SetRescueBuildingColorChangeBJ(bool changeColor);
        [NativeLuaMemberAttribute]
        public static extern void MakeUnitRescuableToForceBJEnum();
        [NativeLuaMemberAttribute]
        public static extern void MakeUnitRescuableToForceBJ(unit whichUnit, bool isRescuable, force whichForce);
        [NativeLuaMemberAttribute]
        public static extern void InitRescuableBehaviorBJ();
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerTechResearchedSwap(int techid, int levels, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerTechMaxAllowedSwap(int techid, int maximum, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerMaxHeroesAllowed(int maximum, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern int GetPlayerTechCountSimple(int techid, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern int GetPlayerTechMaxAllowedSwap(int techid, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerAbilityAvailableBJ(bool avail, int abilid, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetCampaignMenuRaceBJ(int campaignNumber);
        [NativeLuaMemberAttribute]
        public static extern void SetMissionAvailableBJ(bool available, int missionIndex);
        [NativeLuaMemberAttribute]
        public static extern void SetCampaignAvailableBJ(bool available, int campaignNumber);
        [NativeLuaMemberAttribute]
        public static extern void SetCinematicAvailableBJ(bool available, int cinematicIndex);
        [NativeLuaMemberAttribute]
        public static extern gamecache InitGameCacheBJ(string campaignFile);
        [NativeLuaMemberAttribute]
        public static extern bool SaveGameCacheBJ(gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern gamecache GetLastCreatedGameCacheBJ();
        [NativeLuaMemberAttribute]
        public static extern hashtable InitHashtableBJ();
        [NativeLuaMemberAttribute]
        public static extern hashtable GetLastCreatedHashtableBJ();
        [NativeLuaMemberAttribute]
        public static extern void StoreRealBJ(float value, string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern void StoreIntegerBJ(int value, string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern void StoreBooleanBJ(bool value, string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern bool StoreStringBJ(string value, string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern bool StoreUnitBJ(unit whichUnit, string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern void SaveRealBJ(float value, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern void SaveIntegerBJ(int value, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern void SaveBooleanBJ(bool value, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveStringBJ(string value, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SavePlayerHandleBJ(player whichPlayer, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveWidgetHandleBJ(widget whichWidget, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveDestructableHandleBJ(destructable whichDestructable, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveItemHandleBJ(item whichItem, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveUnitHandleBJ(unit whichUnit, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveAbilityHandleBJ(ability whichAbility, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTimerHandleBJ(timer whichTimer, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTriggerHandleBJ(trigger whichTrigger, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTriggerConditionHandleBJ(triggercondition whichTriggercondition, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTriggerActionHandleBJ(triggeraction whichTriggeraction, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTriggerEventHandleBJ(@event whichEvent, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveForceHandleBJ(force whichForce, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveGroupHandleBJ(group whichGroup, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveLocationHandleBJ(location whichLocation, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveRectHandleBJ(rect whichRect, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveBooleanExprHandleBJ(boolexpr whichBoolexpr, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveSoundHandleBJ(sound whichSound, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveEffectHandleBJ(effect whichEffect, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveUnitPoolHandleBJ(unitpool whichUnitpool, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveItemPoolHandleBJ(itempool whichItempool, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveQuestHandleBJ(quest whichQuest, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveQuestItemHandleBJ(questitem whichQuestitem, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveDefeatConditionHandleBJ(defeatcondition whichDefeatcondition, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTimerDialogHandleBJ(timerdialog whichTimerdialog, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveLeaderboardHandleBJ(leaderboard whichLeaderboard, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveMultiboardHandleBJ(multiboard whichMultiboard, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveMultiboardItemHandleBJ(multiboarditem whichMultiboarditem, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTrackableHandleBJ(trackable whichTrackable, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveDialogHandleBJ(dialog whichDialog, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveButtonHandleBJ(button whichButton, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveTextTagHandleBJ(texttag whichTexttag, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveLightningHandleBJ(lightning whichLightning, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveImageHandleBJ(image whichImage, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveUbersplatHandleBJ(ubersplat whichUbersplat, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveRegionHandleBJ(region whichRegion, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveFogStateHandleBJ(fogstate whichFogState, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveFogModifierHandleBJ(fogmodifier whichFogModifier, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveAgentHandleBJ(agent whichAgent, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool SaveHashtableHandleBJ(hashtable whichHashtable, int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern float GetStoredRealBJ(string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern int GetStoredIntegerBJ(string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern bool GetStoredBooleanBJ(string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern string GetStoredStringBJ(string key, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern float LoadRealBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern int LoadIntegerBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool LoadBooleanBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern string LoadStringBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern player LoadPlayerHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern widget LoadWidgetHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern destructable LoadDestructableHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern item LoadItemHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern unit LoadUnitHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern ability LoadAbilityHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern timer LoadTimerHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern trigger LoadTriggerHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern triggercondition LoadTriggerConditionHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern triggeraction LoadTriggerActionHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern @event LoadTriggerEventHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern force LoadForceHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern group LoadGroupHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern location LoadLocationHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern rect LoadRectHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern boolexpr LoadBooleanExprHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern sound LoadSoundHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern effect LoadEffectHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern unitpool LoadUnitPoolHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern itempool LoadItemPoolHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern quest LoadQuestHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern questitem LoadQuestItemHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern defeatcondition LoadDefeatConditionHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern timerdialog LoadTimerDialogHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern leaderboard LoadLeaderboardHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern multiboard LoadMultiboardHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern multiboarditem LoadMultiboardItemHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern trackable LoadTrackableHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern dialog LoadDialogHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern button LoadButtonHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern texttag LoadTextTagHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern lightning LoadLightningHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern image LoadImageHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern ubersplat LoadUbersplatHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern region LoadRegionHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern fogstate LoadFogStateHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern fogmodifier LoadFogModifierHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern hashtable LoadHashtableHandleBJ(int key, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern unit RestoreUnitLocFacingAngleBJ(string key, string missionKey, gamecache cache, player forWhichPlayer, location loc, float facing);
        [NativeLuaMemberAttribute]
        public static extern unit RestoreUnitLocFacingPointBJ(string key, string missionKey, gamecache cache, player forWhichPlayer, location loc, location lookAt);
        [NativeLuaMemberAttribute]
        public static extern unit GetLastRestoredUnitBJ();
        [NativeLuaMemberAttribute]
        public static extern void FlushGameCacheBJ(gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern void FlushStoredMissionBJ(string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern void FlushParentHashtableBJ(hashtable table);
        [NativeLuaMemberAttribute]
        public static extern void FlushChildHashtableBJ(int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern bool HaveStoredValue(string key, int valueType, string missionKey, gamecache cache);
        [NativeLuaMemberAttribute]
        public static extern bool HaveSavedValue(int key, int valueType, int missionKey, hashtable table);
        [NativeLuaMemberAttribute]
        public static extern void ShowCustomCampaignButton(bool show, int whichButton);
        [NativeLuaMemberAttribute]
        public static extern bool IsCustomCampaignButtonVisibile(int whichButton);
        [NativeLuaMemberAttribute]
        public static extern void LoadGameBJ(string loadFileName, bool doScoreScreen);
        [NativeLuaMemberAttribute]
        public static extern void SaveAndChangeLevelBJ(string saveFileName, string newLevel, bool doScoreScreen);
        [NativeLuaMemberAttribute]
        public static extern void SaveAndLoadGameBJ(string saveFileName, string loadFileName, bool doScoreScreen);
        [NativeLuaMemberAttribute]
        public static extern bool RenameSaveDirectoryBJ(string sourceDirName, string destDirName);
        [NativeLuaMemberAttribute]
        public static extern bool RemoveSaveDirectoryBJ(string sourceDirName);
        [NativeLuaMemberAttribute]
        public static extern bool CopySaveGameBJ(string sourceSaveName, string destSaveName);
        [NativeLuaMemberAttribute]
        public static extern float GetPlayerStartLocationX(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern float GetPlayerStartLocationY(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern location GetPlayerStartLocationLoc(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern location GetRectCenter(rect whichRect);
        [NativeLuaMemberAttribute]
        public static extern bool IsPlayerSlotState(player whichPlayer, playerslotstate whichState);
        [NativeLuaMemberAttribute]
        public static extern int GetFadeFromSeconds(float seconds);
        [NativeLuaMemberAttribute]
        public static extern float GetFadeFromSecondsAsReal(float seconds);
        [NativeLuaMemberAttribute]
        public static extern void AdjustPlayerStateSimpleBJ(player whichPlayer, playerstate whichPlayerState, int delta);
        [NativeLuaMemberAttribute]
        public static extern void AdjustPlayerStateBJ(int delta, player whichPlayer, playerstate whichPlayerState);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerStateBJ(player whichPlayer, playerstate whichPlayerState, int value);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerFlagBJ(playerstate whichPlayerFlag, bool flag, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerTaxRateBJ(int rate, playerstate whichResource, player sourcePlayer, player otherPlayer);
        [NativeLuaMemberAttribute]
        public static extern int GetPlayerTaxRateBJ(playerstate whichResource, player sourcePlayer, player otherPlayer);
        [NativeLuaMemberAttribute]
        public static extern bool IsPlayerFlagSetBJ(playerstate whichPlayerFlag, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void AddResourceAmountBJ(int delta, unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern int GetConvertedPlayerId(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern player ConvertedPlayer(int convertedPlayerId);
        [NativeLuaMemberAttribute]
        public static extern float GetRectWidthBJ(rect r);
        [NativeLuaMemberAttribute]
        public static extern float GetRectHeightBJ(rect r);
        [NativeLuaMemberAttribute]
        public static extern unit BlightGoldMineForPlayerBJ(unit goldMine, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern unit BlightGoldMineForPlayer(unit goldMine, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern unit GetLastHauntedGoldMine();
        [NativeLuaMemberAttribute]
        public static extern bool IsPointBlightedBJ(location where);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerColorBJEnum();
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerColorBJ(player whichPlayer, playercolor color, bool changeExisting);
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerUnitAvailableBJ(int unitId, bool allowed, player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void LockGameSpeedBJ();
        [NativeLuaMemberAttribute]
        public static extern void UnlockGameSpeedBJ();
        [NativeLuaMemberAttribute]
        public static extern bool IssueTargetOrderBJ(unit whichUnit, string order, widget targetWidget);
        [NativeLuaMemberAttribute]
        public static extern bool IssuePointOrderLocBJ(unit whichUnit, string order, location whichLocation);
        [NativeLuaMemberAttribute]
        public static extern bool IssueTargetDestructableOrder(unit whichUnit, string order, widget targetWidget);
        [NativeLuaMemberAttribute]
        public static extern bool IssueTargetItemOrder(unit whichUnit, string order, widget targetWidget);
        [NativeLuaMemberAttribute]
        public static extern bool IssueImmediateOrderBJ(unit whichUnit, string order);
        [NativeLuaMemberAttribute]
        public static extern bool GroupTargetOrderBJ(group whichGroup, string order, widget targetWidget);
        [NativeLuaMemberAttribute]
        public static extern bool GroupPointOrderLocBJ(group whichGroup, string order, location whichLocation);
        [NativeLuaMemberAttribute]
        public static extern bool GroupImmediateOrderBJ(group whichGroup, string order);
        [NativeLuaMemberAttribute]
        public static extern bool GroupTargetDestructableOrder(group whichGroup, string order, widget targetWidget);
        [NativeLuaMemberAttribute]
        public static extern bool GroupTargetItemOrder(group whichGroup, string order, widget targetWidget);
        [NativeLuaMemberAttribute]
        public static extern destructable GetDyingDestructable();
        [NativeLuaMemberAttribute]
        public static extern void SetUnitRallyPoint(unit whichUnit, location targPos);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitRallyUnit(unit whichUnit, unit targUnit);
        [NativeLuaMemberAttribute]
        public static extern void SetUnitRallyDestructable(unit whichUnit, destructable targDest);
        [NativeLuaMemberAttribute]
        public static extern void SaveDyingWidget();
        [NativeLuaMemberAttribute]
        public static extern void SetBlightRectBJ(bool addBlight, player whichPlayer, rect r);
        [NativeLuaMemberAttribute]
        public static extern void SetBlightRadiusLocBJ(bool addBlight, player whichPlayer, location loc, float radius);
        [NativeLuaMemberAttribute]
        public static extern string GetAbilityName(int abilcode);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingVisibility();
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingResources();
        [NativeLuaMemberAttribute]
        public static extern void ReducePlayerTechMaxAllowed(player whichPlayer, int techId, int limit);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingHeroLimit();
        [NativeLuaMemberAttribute]
        public static extern bool MeleeTrainedUnitIsHeroBJFilter();
        [NativeLuaMemberAttribute]
        public static extern void MeleeGrantItemsToHero(unit whichUnit);
        [NativeLuaMemberAttribute]
        public static extern void MeleeGrantItemsToTrainedHero();
        [NativeLuaMemberAttribute]
        public static extern void MeleeGrantItemsToHiredHero();
        [NativeLuaMemberAttribute]
        public static extern void MeleeGrantHeroItems();
        [NativeLuaMemberAttribute]
        public static extern void MeleeClearExcessUnit();
        [NativeLuaMemberAttribute]
        public static extern void MeleeClearNearbyUnits(float x, float y, float range);
        [NativeLuaMemberAttribute]
        public static extern void MeleeClearExcessUnits();
        [NativeLuaMemberAttribute]
        public static extern void MeleeEnumFindNearestMine();
        [NativeLuaMemberAttribute]
        public static extern unit MeleeFindNearestMine(location src, float range);
        [NativeLuaMemberAttribute]
        public static extern unit MeleeRandomHeroLoc(player p, int id1, int id2, int id3, int id4, location loc);
        [NativeLuaMemberAttribute]
        public static extern location MeleeGetProjectedLoc(location src, location targ, float distance, float deltaAngle);
        [NativeLuaMemberAttribute]
        public static extern float MeleeGetNearestValueWithin(float val, float minVal, float maxVal);
        [NativeLuaMemberAttribute]
        public static extern location MeleeGetLocWithinRect(location src, rect r);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnitsHuman(player whichPlayer, location startLoc, bool doHeroes, bool doCamera, bool doPreload);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnitsOrc(player whichPlayer, location startLoc, bool doHeroes, bool doCamera, bool doPreload);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnitsUndead(player whichPlayer, location startLoc, bool doHeroes, bool doCamera, bool doPreload);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnitsNightElf(player whichPlayer, location startLoc, bool doHeroes, bool doCamera, bool doPreload);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnitsUnknownRace(player whichPlayer, location startLoc, bool doHeroes, bool doCamera, bool doPreload);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnits();
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingUnitsForPlayer(race whichRace, player whichPlayer, location loc, bool doHeroes);
        [NativeLuaMemberAttribute]
        public static extern void PickMeleeAI(player num, string s1, string s2, string s3);
        [NativeLuaMemberAttribute]
        public static extern void MeleeStartingAI();
        [NativeLuaMemberAttribute]
        public static extern void LockGuardPosition(unit targ);
        [NativeLuaMemberAttribute]
        public static extern bool MeleePlayerIsOpponent(int playerIndex, int opponentIndex);
        [NativeLuaMemberAttribute]
        public static extern int MeleeGetAllyStructureCount(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern int MeleeGetAllyCount(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern int MeleeGetAllyKeyStructureCount(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MeleeDoDrawEnum();
        [NativeLuaMemberAttribute]
        public static extern void MeleeDoVictoryEnum();
        [NativeLuaMemberAttribute]
        public static extern void MeleeDoDefeat(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MeleeDoDefeatEnum();
        [NativeLuaMemberAttribute]
        public static extern void MeleeDoLeave(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MeleeRemoveObservers();
        [NativeLuaMemberAttribute]
        public static extern force MeleeCheckForVictors();
        [NativeLuaMemberAttribute]
        public static extern void MeleeCheckForLosersAndVictors();
        [NativeLuaMemberAttribute]
        public static extern string MeleeGetCrippledWarningMessage(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern string MeleeGetCrippledTimerMessage(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern string MeleeGetCrippledRevealedMessage(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MeleeExposePlayer(player whichPlayer, bool expose);
        [NativeLuaMemberAttribute]
        public static extern void MeleeExposeAllPlayers();
        [NativeLuaMemberAttribute]
        public static extern void MeleeCrippledPlayerTimeout();
        [NativeLuaMemberAttribute]
        public static extern bool MeleePlayerIsCrippled(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MeleeCheckForCrippledPlayers();
        [NativeLuaMemberAttribute]
        public static extern void MeleeCheckLostUnit(unit lostUnit);
        [NativeLuaMemberAttribute]
        public static extern void MeleeCheckAddedUnit(unit addedUnit);
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerActionConstructCancel();
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerActionUnitDeath();
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerActionUnitConstructionStart();
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerActionPlayerDefeated();
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerActionPlayerLeft();
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerActionAllianceChange();
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerTournamentFinishSoon();
        [NativeLuaMemberAttribute]
        public static extern bool MeleeWasUserPlayer(player whichPlayer);
        [NativeLuaMemberAttribute]
        public static extern void MeleeTournamentFinishNowRuleA(int multiplier);
        [NativeLuaMemberAttribute]
        public static extern void MeleeTriggerTournamentFinishNow();
        [NativeLuaMemberAttribute]
        public static extern void MeleeInitVictoryDefeat();
        [NativeLuaMemberAttribute]
        public static extern void CheckInitPlayerSlotAvailability();
        [NativeLuaMemberAttribute]
        public static extern void SetPlayerSlotAvailable(player whichPlayer, mapcontrol control);
        [NativeLuaMemberAttribute]
        public static extern void TeamInitPlayerSlots(int teamCount);
        [NativeLuaMemberAttribute]
        public static extern void MeleeInitPlayerSlots();
        [NativeLuaMemberAttribute]
        public static extern void FFAInitPlayerSlots();
        [NativeLuaMemberAttribute]
        public static extern void OneOnOneInitPlayerSlots();
        [NativeLuaMemberAttribute]
        public static extern void InitGenericPlayerSlots();
        [NativeLuaMemberAttribute]
        public static extern void SetDNCSoundsDawn();
        [NativeLuaMemberAttribute]
        public static extern void SetDNCSoundsDusk();
        [NativeLuaMemberAttribute]
        public static extern void SetDNCSoundsDay();
        [NativeLuaMemberAttribute]
        public static extern void SetDNCSoundsNight();
        [NativeLuaMemberAttribute]
        public static extern void InitDNCSounds();
        [NativeLuaMemberAttribute]
        public static extern void InitBlizzardGlobals();
        [NativeLuaMemberAttribute]
        public static extern void InitQueuedTriggers();
        [NativeLuaMemberAttribute]
        public static extern void InitMapRects();
        [NativeLuaMemberAttribute]
        public static extern void InitSummonableCaps();
        [NativeLuaMemberAttribute]
        public static extern void UpdateStockAvailability(item whichItem);
        [NativeLuaMemberAttribute]
        public static extern void UpdateEachStockBuildingEnum();
        [NativeLuaMemberAttribute]
        public static extern void UpdateEachStockBuilding(itemtype iType, int iLevel);
        [NativeLuaMemberAttribute]
        public static extern void PerformStockUpdates();
        [NativeLuaMemberAttribute]
        public static extern void StartStockUpdates();
        [NativeLuaMemberAttribute]
        public static extern void RemovePurchasedItem();
        [NativeLuaMemberAttribute]
        public static extern void InitNeutralBuildings();
        [NativeLuaMemberAttribute]
        public static extern void MarkGameStarted();
        [NativeLuaMemberAttribute]
        public static extern void DetectGameStarted();
        [NativeLuaMemberAttribute]
        public static extern void InitBlizzard();
        [NativeLuaMemberAttribute]
        public static extern void RandomDistReset();
        [NativeLuaMemberAttribute]
        public static extern void RandomDistAddItem(int inID, int inChance);
        [NativeLuaMemberAttribute]
        public static extern int RandomDistChoose();
        [NativeLuaMemberAttribute]
        public static extern item UnitDropItem(unit inUnit, int inItemID);
        [NativeLuaMemberAttribute]
        public static extern item WidgetDropItem(widget inWidget, int inItemID);
        [NativeLuaMemberAttribute]
        public static extern bool BlzIsLastInstanceObjectFunctionSuccessful();
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityBooleanFieldBJ(ability whichAbility, abilitybooleanfield whichField, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityIntegerFieldBJ(ability whichAbility, abilityintegerfield whichField, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityRealFieldBJ(ability whichAbility, abilityrealfield whichField, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityStringFieldBJ(ability whichAbility, abilitystringfield whichField, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityBooleanLevelFieldBJ(ability whichAbility, abilitybooleanlevelfield whichField, int level, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityIntegerLevelFieldBJ(ability whichAbility, abilityintegerlevelfield whichField, int level, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityRealLevelFieldBJ(ability whichAbility, abilityreallevelfield whichField, int level, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityStringLevelFieldBJ(ability whichAbility, abilitystringlevelfield whichField, int level, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityBooleanLevelArrayFieldBJ(ability whichAbility, abilitybooleanlevelarrayfield whichField, int level, int index, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityIntegerLevelArrayFieldBJ(ability whichAbility, abilityintegerlevelarrayfield whichField, int level, int index, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityRealLevelArrayFieldBJ(ability whichAbility, abilityreallevelarrayfield whichField, int level, int index, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetAbilityStringLevelArrayFieldBJ(ability whichAbility, abilitystringlevelarrayfield whichField, int level, int index, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzAddAbilityBooleanLevelArrayFieldBJ(ability whichAbility, abilitybooleanlevelarrayfield whichField, int level, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzAddAbilityIntegerLevelArrayFieldBJ(ability whichAbility, abilityintegerlevelarrayfield whichField, int level, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzAddAbilityRealLevelArrayFieldBJ(ability whichAbility, abilityreallevelarrayfield whichField, int level, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzAddAbilityStringLevelArrayFieldBJ(ability whichAbility, abilitystringlevelarrayfield whichField, int level, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzRemoveAbilityBooleanLevelArrayFieldBJ(ability whichAbility, abilitybooleanlevelarrayfield whichField, int level, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzRemoveAbilityIntegerLevelArrayFieldBJ(ability whichAbility, abilityintegerlevelarrayfield whichField, int level, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzRemoveAbilityRealLevelArrayFieldBJ(ability whichAbility, abilityreallevelarrayfield whichField, int level, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzRemoveAbilityStringLevelArrayFieldBJ(ability whichAbility, abilitystringlevelarrayfield whichField, int level, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzItemAddAbilityBJ(item whichItem, int abilCode);
        [NativeLuaMemberAttribute]
        public static extern void BlzItemRemoveAbilityBJ(item whichItem, int abilCode);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetItemBooleanFieldBJ(item whichItem, itembooleanfield whichField, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetItemIntegerFieldBJ(item whichItem, itemintegerfield whichField, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetItemRealFieldBJ(item whichItem, itemrealfield whichField, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetItemStringFieldBJ(item whichItem, itemstringfield whichField, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitBooleanFieldBJ(unit whichUnit, unitbooleanfield whichField, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitIntegerFieldBJ(unit whichUnit, unitintegerfield whichField, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitRealFieldBJ(unit whichUnit, unitrealfield whichField, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitStringFieldBJ(unit whichUnit, unitstringfield whichField, string value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitWeaponBooleanFieldBJ(unit whichUnit, unitweaponbooleanfield whichField, int index, bool value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitWeaponIntegerFieldBJ(unit whichUnit, unitweaponintegerfield whichField, int index, int value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitWeaponRealFieldBJ(unit whichUnit, unitweaponrealfield whichField, int index, float value);
        [NativeLuaMemberAttribute]
        public static extern void BlzSetUnitWeaponStringFieldBJ(unit whichUnit, unitweaponstringfield whichField, int index, string value);
    }
}