// ------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using War3Api.Object.Enums;

using War3Net.Build.Extensions;
using War3Net.Build.Object;
using War3Net.Common.Extensions;

namespace War3Api.Object.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFallbackWithCampaign()
        {
            var campaignDb = new ObjectDatabase(null);
            var mapDb = new ObjectDatabase(campaignDb);

            var unitCampaign1 = new Unit(UnitType.Paladin, campaignDb);
            var unitCampaign2 = new Unit(UnitType.Paladin, "H002", campaignDb);

            var unitMap1 = new Unit(UnitType.Paladin, mapDb);
            var unitMap2 = new Unit(UnitType.Paladin, "H002", mapDb);
            var unitMap3 = new Unit(UnitType.Paladin, "H003", mapDb);

            unitCampaign1.StatsHitPointsMaximumBase = 1000;
            unitCampaign1.StatsManaMaximum = 2000;

            unitCampaign2.StatsStartingStrength = 200;
            unitCampaign2.StatsStartingAgility = 250;

            unitMap1.StatsManaMaximum = 3000;

            unitMap2.StatsStartingAgility = 200;

            unitMap3.StatsStartingIntelligence = 300;

            Assert.AreEqual(unitCampaign1.StatsHitPointsMaximumBase, 1000);
            Assert.AreEqual(unitCampaign1.StatsManaMaximum, 2000);
            Assert.ThrowsException<KeyNotFoundException>(() => unitCampaign1.StatsStartingStrength);
            Assert.ThrowsException<KeyNotFoundException>(() => unitCampaign1.StatsStartingAgility);
            Assert.ThrowsException<KeyNotFoundException>(() => unitCampaign1.StatsStartingIntelligence);
            Assert.IsTrue(unitCampaign1.IsStatsHitPointsMaximumBaseModified);
            Assert.IsTrue(unitCampaign1.IsStatsManaMaximumModified);
            Assert.IsFalse(unitCampaign1.IsStatsStartingStrengthModified);
            Assert.IsFalse(unitCampaign1.IsStatsStartingAgilityModified);
            Assert.IsFalse(unitCampaign1.IsStatsStartingIntelligenceModified);

            Assert.ThrowsException<KeyNotFoundException>(() => unitCampaign2.StatsHitPointsMaximumBase);
            Assert.ThrowsException<KeyNotFoundException>(() => unitCampaign2.StatsManaMaximum);
            Assert.AreEqual(unitCampaign2.StatsStartingStrength, 200);
            Assert.AreEqual(unitCampaign2.StatsStartingAgility, 250);
            Assert.ThrowsException<KeyNotFoundException>(() => unitCampaign2.StatsStartingIntelligence);
            Assert.IsFalse(unitCampaign2.IsStatsHitPointsMaximumBaseModified);
            Assert.IsFalse(unitCampaign2.IsStatsManaMaximumModified);
            Assert.IsTrue(unitCampaign2.IsStatsStartingStrengthModified);
            Assert.IsTrue(unitCampaign2.IsStatsStartingAgilityModified);
            Assert.IsFalse(unitCampaign2.IsStatsStartingIntelligenceModified);

            Assert.AreEqual(unitMap1.StatsHitPointsMaximumBase, 1000);
            Assert.AreEqual(unitMap1.StatsManaMaximum, 3000);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap1.StatsStartingStrength);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap1.StatsStartingAgility);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap1.StatsStartingIntelligence);
            Assert.IsFalse(unitMap1.IsStatsHitPointsMaximumBaseModified);
            Assert.IsTrue(unitMap1.IsStatsManaMaximumModified);
            Assert.IsFalse(unitMap1.IsStatsStartingStrengthModified);
            Assert.IsFalse(unitMap1.IsStatsStartingAgilityModified);
            Assert.IsFalse(unitMap1.IsStatsStartingIntelligenceModified);

            Assert.ThrowsException<KeyNotFoundException>(() => unitMap2.StatsHitPointsMaximumBase);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap2.StatsManaMaximum);
            Assert.AreEqual(unitMap2.StatsStartingStrength, 200);
            Assert.AreEqual(unitMap2.StatsStartingAgility, 200);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap2.StatsStartingIntelligence);
            Assert.IsFalse(unitMap2.IsStatsHitPointsMaximumBaseModified);
            Assert.IsFalse(unitMap2.IsStatsManaMaximumModified);
            Assert.IsFalse(unitMap2.IsStatsStartingStrengthModified);
            Assert.IsTrue(unitMap2.IsStatsStartingAgilityModified);
            Assert.IsFalse(unitMap2.IsStatsStartingIntelligenceModified);

            Assert.ThrowsException<KeyNotFoundException>(() => unitMap3.StatsHitPointsMaximumBase);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap3.StatsManaMaximum);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap3.StatsStartingStrength);
            Assert.ThrowsException<KeyNotFoundException>(() => unitMap3.StatsStartingAgility);
            Assert.AreEqual(unitMap3.StatsStartingIntelligence, 300);
            Assert.IsFalse(unitMap3.IsStatsHitPointsMaximumBaseModified);
            Assert.IsFalse(unitMap3.IsStatsManaMaximumModified);
            Assert.IsFalse(unitMap3.IsStatsStartingStrengthModified);
            Assert.IsFalse(unitMap3.IsStatsStartingAgilityModified);
            Assert.IsTrue(unitMap3.IsStatsStartingIntelligenceModified);
        }

        [TestMethod]
        public void TestDefaultDatabaseSingleton()
        {
            var u = new Unit(UnitType.Peasant);
            Assert.AreEqual(u.Db, ObjectDatabase.DefaultDatabase);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var db = new ObjectDatabase(Array.Empty<int>(), null);

            var peasant = new Unit(UnitType.Peasant, db);
            // Assert.AreEqual(1, ObjectDatabase.DefaultDatabase.ObjectCount);

            peasant.CombatAcquisitionRange = 123;
            Assert.AreEqual(123, peasant.CombatAcquisitionRange);

            peasant.CombatArmorType = ArmorType.Metal;
            Assert.AreEqual(ArmorType.Metal.ToString(), peasant.CombatArmorTypeRaw);

            var abilities = new List<Ability>();
            // abilities.Add(new Ability(AbilityType.RepairHuman));
            abilities.Add(new Abilities.RepairHuman(db));
            var up = new Abilities.TankUpgrade("A000", db);
            up.DataNewUnitType[1] = peasant;

            peasant.AbilitiesNormal = abilities;
            Assert.AreEqual(((int)AbilityType.RepairHuman).ToRawcode(), peasant.AbilitiesNormalRaw);
        }

        [TestMethod]
        public void TestEnums()
        {
            var db = new ObjectDatabase(Array.Empty<int>(), null);

            const string TestFilePath = @"..\..\..\TestData\TestEnums.w3o";

            using var fileStream = File.OpenRead(TestFilePath);
            using var binaryReader = new BinaryReader(fileStream);

            var objectData = binaryReader.ReadObjectData(false);

            var errorLines = new List<string>();

            var castType = new Dictionary<Type, ObjectDataType>
            {
                { typeof(int), ObjectDataType.Int },
                { typeof(string), ObjectDataType.String },
            };

            // Verify unit data
            var unitData = objectData.UnitData;

            var peasant = new Unit(UnitType.Peasant, db);
            peasant.ArtShadowImageUnit = ShadowImage.ShadowFlyer;
            peasant.CombatArmorType = ArmorType.Stone;
            peasant.CombatAttack1AttackType = AttackType.Hero;
            peasant.CombatAttacksEnabled = AttackBits.Attack2Only;
            peasant.PathingAIPlacementType = AiBuffer.Resource;
            peasant.ArtTeamColor = TeamColor.Player1Red;

            var paladin = new Unit(UnitType.Paladin, db);
            paladin.CombatAttack2WeaponSound = CombatSound.MetalLightChop;
            paladin.CombatDeathType = DeathType.CanRaiseDoesDecay;
            paladin.CombatDefenseType = DefenseType.Normal;
            paladin.MovementType = MoveType.Amph;
            paladin.StatsPrimaryAttribute = AttributeType.AGI;

            var altar = new Unit(UnitType.Altarofkings, db);
            altar.ArtModelFileExtraVersions = VersionFlags.ReignOfChaos | VersionFlags.TheFrozenThrone;
            altar.CombatAttack1WeaponType = WeaponType.Mline;
            altar.PathingPlacementPreventedBy = new[] { PathingType.Unbuildable, PathingType.Blighted };
            altar.PathingPlacementRequires = Array.Empty<PathingType>();
            altar.StatsHitPointsRegenerationType = RegenType.Night;
            altar.StatsRace = UnitRace.Other;
            altar.StatsUnitClassification = new[] { UnitClassification.Ancient, UnitClassification.Mechanical, UnitClassification.Peon };

            var expectUnit = new Dictionary<string, ObjectDataType>()
            {
                // Peasant
                { "ushu", castType[peasant.ArtShadowImageUnitRaw.GetType()] },
                { "uarm", castType[peasant.CombatArmorTypeRaw.GetType()] },
                { "ua1t", castType[peasant.CombatAttack1AttackTypeRaw.GetType()] },
                { "uaen", castType[peasant.CombatAttacksEnabledRaw.GetType()] },
                { "uabt", castType[peasant.PathingAIPlacementTypeRaw.GetType()] },
                { "utco", castType[peasant.ArtTeamColorRaw.GetType()] },

                // Paladin
                { "ucs2", castType[paladin.CombatAttack2WeaponSoundRaw.GetType()] },
                { "udea", castType[paladin.CombatDeathTypeRaw.GetType()] },
                { "udty", castType[paladin.CombatDefenseTypeRaw.GetType()] },
                { "umvt", castType[paladin.MovementTypeRaw.GetType()] },
                { "upra", castType[paladin.StatsPrimaryAttributeRaw.GetType()] },

                // Altar
                { "uver", castType[altar.ArtModelFileExtraVersionsRaw.GetType()] },
                { "ua1w", castType[altar.CombatAttack1WeaponTypeRaw.GetType()] },
                { "upar", castType[altar.PathingPlacementPreventedByRaw.GetType()] },
                { "upap", castType[altar.PathingPlacementRequiresRaw.GetType()] },
                { "uhrt", castType[altar.StatsHitPointsRegenerationTypeRaw.GetType()] },
                { "urac", castType[altar.StatsRaceRaw.GetType()] },
                { "utyp", castType[altar.StatsUnitClassificationRaw.GetType()] },
            };

            foreach (var unit in objectData.GetAllUnits())
            {
                var actualUnit = db.GetUnit(unit.OldId);
                foreach (var mod in unit.Modifications)
                {
                    if (expectUnit.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualUnit.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify item data
            var itemData = objectData.ItemData;

            var gauntlets = new Item(ItemType.GauntletsOfOgreStrength3, db);
            gauntlets.StatsClassification = ItemClass.Miscellaneous;

            var expectItem = new Dictionary<string, ObjectDataType>()
            {
                // Gauntlets
                { "icla", castType[gauntlets.StatsClassificationRaw.GetType()] },
            };

            foreach (var item in objectData.GetAllItems())
            {
                var actualItem = db.GetItem(item.OldId);
                foreach (var mod in item.Modifications)
                {
                    if (expectItem.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualItem.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify destructable data
            var destructableData = objectData.DestructableData;

            var stoneDoor = new Destructable(DestructableType.RollingStoneDoor_ZTd6, db);
            stoneDoor.EditorCategory = DestructableCategory.BridgesRamps;
            stoneDoor.EditorTilesets = new[] { Tileset.LordaeronFall, Tileset.LordaeronSummer,Tileset.SunkenRuins };

            var expectDestructable = new Dictionary<string, ObjectDataType>()
            {
                // Stone door
                { "bcat", castType[stoneDoor.EditorCategoryRaw.GetType()] },
                { "btil", castType[stoneDoor.EditorTilesetsRaw.GetType()] },
            };

            foreach (var destructable in objectData.GetAllDestructables())
            {
                var actualDestructable = db.GetDestructable(destructable.OldId);
                foreach (var mod in destructable.Modifications)
                {
                    if (expectDestructable.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualDestructable.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify doodad data
            var doodadData = objectData.DoodadData;

            var trough = new Doodad(DoodadType.Trough, db);
            trough.EditorCategory = DoodadCategory.Cinematic;

            var expectDoodad = new Dictionary<string, ObjectDataType>()
            {
                // Trough
                { "dcat", castType[trough.EditorCategoryRaw.GetType()] },
            };

            foreach (var doodad in objectData.GetAllDoodads())
            {
                var actualDoodad = db.GetDoodad(doodad.OldId);
                foreach (var mod in doodad.Modifications)
                {
                    if (expectDoodad.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualDoodad.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify ability data
            var abilityData = objectData.AbilityData;

            var darkRangerSilence = new Abilities.DarkRangerSilence(db);
            darkRangerSilence.DataAttacksPrevented[1] = SilenceFlags.Melee | SilenceFlags.Ranged | SilenceFlags.Special | SilenceFlags.Spells;

            var demonHunterMetamorphosis = new Abilities.DemonHunterMetamorphosis(db);
            demonHunterMetamorphosis.DataMorphingFlags[1] = MorphFlags.RequiresPayment;

            var farseerFarSight = new Abilities.FarseerFarSight(db);
            farseerFarSight.DataDetectionType[1] = DetectionType.Invisible;

            var seaWitchFrostArrows = new Abilities.SeaWitchFrostArrows(db);
            seaWitchFrostArrows.DataStackFlags[1] = 0;

            var illidanChannel = new Abilities.IllidanChannel(db);
            illidanChannel.DataTargetType[1] = ChannelType.UnitOrPointTarget;
            illidanChannel.DataOptions[1] = ChannelFlags.UniversalSpell | ChannelFlags.UniqueCast;

            var alliedBuilding = new Abilities.AlliedBuilding(db);
            alliedBuilding.DataInteractionType[1] = InteractionFlags.AnyUnitWInventory | InteractionFlags.AnyNonBuilding;

            var rejuvination = new Abilities.Rejuvination(db);
            rejuvination.DataAllowWhenFull[1] = FullFlags.ManaOnly;

            var rootAncientProtector = new Abilities.RootAncientProtector(db);
            rootAncientProtector.DataUprootedDefenseType[1] = DefenseTypeInt.Normal;

            var preservation = new Abilities.Preservation(db);
            preservation.DataBuildingTypesAllowed[1] = PickFlags.General;

            var expectAbility = new Dictionary<string, ObjectDataType>()
            {
                // Dark ranger silence
                { "Nsi1", castType[darkRangerSilence.DataAttacksPreventedRaw[1].GetType()] },

                // Demon hunter metamorphosis
                { "Eme2", castType[demonHunterMetamorphosis.DataMorphingFlagsRaw[1].GetType()] },

                // Farseer far sight
                { "Ofs1", castType[farseerFarSight.DataDetectionTypeRaw[1].GetType()] },

                // Sea witch frost arrows
                { "Hca4", castType[seaWitchFrostArrows.DataStackFlagsRaw[1].GetType()] },

                // Illidan channel
                { "Ncl2", castType[illidanChannel.DataTargetTypeRaw[1].GetType()] },
                { "Ncl3", castType[illidanChannel.DataOptionsRaw[1].GetType()] },

                // Allied building
                { "Neu2", castType[alliedBuilding.DataInteractionTypeRaw[1].GetType()] },

                // Rejuvination
                { "Rej3", castType[rejuvination.DataAllowWhenFullRaw[1].GetType()] },

                // Root ancient protector
                { "Roo4", castType[rootAncientProtector.DataUprootedDefenseTypeRaw[1].GetType()] },

                // Preservation
                { "Npr1", castType[preservation.DataBuildingTypesAllowedRaw[1].GetType()] },
            };

            foreach (var ability in objectData.GetAllAbilities())
            {
                var actualAbility = db.GetAbility(ability.OldId) ?? throw new NullReferenceException(ability.OldId.ToRawcode());
                foreach (var mod in ability.Modifications)
                {
                    if (expectAbility.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = mod.Level > 0 ? actualAbility.Modifications[mod.Id, mod.Level] : actualAbility.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify buff data
            var buffData = objectData.BuffData;

            var avatar = new Buff(BuffType.Avatar, db);
            avatar.ArtLightning = LightningEffect.DRAB;
            avatar.ArtRequiredSpellDetail = SpellDetail.Medium;

            var expectBuff = new Dictionary<string, ObjectDataType>()
            {
                // Avatar
                { "flig", castType[avatar.ArtLightningRaw.GetType()] },
                { "fspd", castType[avatar.ArtRequiredSpellDetailRaw.GetType()] },
            };

            foreach (var buff in objectData.GetAllBuffs())
            {
                var actualBuff = db.GetBuff(buff.OldId);
                foreach (var mod in buff.Modifications)
                {
                    if (expectBuff.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualBuff.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify upgrade data
            var upgradeData = objectData.UpgradeData;

            var humanAnimalBreeding = new Upgrade(UpgradeType.HumanAnimalBreeding, db);
            humanAnimalBreeding.StatsClass = UpgradeClass.Caster;
            humanAnimalBreeding.DataEffect2 = UpgradeEffect.Radl;

            var expectUpgrade = new Dictionary<string, ObjectDataType>()
            {
                // Human animal breeding
                { "gcls", castType[humanAnimalBreeding.StatsClassRaw.GetType()] },
                { "gef2", castType[humanAnimalBreeding.DataEffect2Raw.GetType()] },
            };

            foreach (var upgrade in objectData.GetAllUpgrades())
            {
                var actualUpgrade = db.GetUpgrade(upgrade.OldId);
                foreach (var mod in upgrade.Modifications)
                {
                    if (expectUpgrade.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualUpgrade.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            Assert.AreEqual(0, errorLines.Count, string.Join("\r\n  ", errorLines.Prepend("\r\nFound one or more errors:")));
        }

        [TestMethod]
        public void TestLists()
        {
            var db = new ObjectDatabase(Array.Empty<int>(), null);

            const string TestFilePath = @"..\..\..\TestData\TestLists.w3o";

            using var fileStream = File.OpenRead(TestFilePath);
            using var binaryReader = new BinaryReader(fileStream);

            var objectData = binaryReader.ReadObjectData(false);

            var errorLines = new List<string>();

            // Verify unit data
            var unitData = objectData.UnitData;

            // Empty lists test
            var peasant = new Unit(UnitType.Peasant, db);
            peasant.TechtreeStructuresBuilt = Array.Empty<Unit>();

            var keep = new Unit(UnitType.Keep, db);
            keep.ArtRequiredAnimationNames = Array.Empty<string>();
            keep.PathingPlacementRequires = Array.Empty<PathingType>();
            keep.TechtreeResearchesAvailable = Array.Empty<Upgrade>();
            keep.StatsUnitClassification = Array.Empty<UnitClassification>();

            var cannonTower = new Unit(UnitType.Cannontower, db);
            cannonTower.TechtreeRequirements = Array.Empty<Tech>();

            var arcaneVault = new Unit(UnitType.Arcanevault, db);
            arcaneVault.TechtreeItemsMade = Array.Empty<Item>();

            var paladin = new Unit(UnitType.Paladin, db);
            paladin.ArtSpecial = Array.Empty<string>();
            paladin.AbilitiesNormal = Array.Empty<Ability>();
            paladin.AbilitiesHero = Array.Empty<Ability>();
            paladin.EditorTilesets = Array.Empty<Tileset>();
            paladin.CombatTargetedAs = Array.Empty<Target>();

            var ziggurat = new Unit(UnitType.Ziggurat, db);
            ziggurat.PathingPlacementPreventedBy = Array.Empty<PathingType>();

            // Non-empty lists test
            var bloodmage = new Unit(UnitType.Bloodmage, db);
            bloodmage.ArtRequiredAnimationNames = new[] { "upgrade", "second" };
            bloodmage.TechtreeStructuresBuilt = new[] { keep, cannonTower, arcaneVault };
            bloodmage.TechtreeItemsSold = new[] { new Item(ItemType.ClawsOfAttack15), new Item(ItemType.CrownOfKings5) };
            bloodmage.TechtreeRequirements = new[] { new Tech(peasant), new Tech(new Upgrade(UpgradeType.HumanAnimalBreeding)), new Tech(TechEquivalent.AnyTier1Hall) };
            bloodmage.TechtreeRequirementsLevels = new[] { 1, 2, 0 };
            bloodmage.ArtSpecial = new[] { @"buildings\other\ElvenFarm\ElvenFarm.mdl", string.Empty };
            bloodmage.AbilitiesNormal = new Ability[] { new Abilities.Alarm(), new Abilities.Inventory() };
            bloodmage.AbilitiesHero = new Ability[] { new Abilities.BloodMageFlameStrike(), new Abilities.BloodMageBanish() };
            bloodmage.EditorTilesets = new[] { Tileset.Ashenvale, Tileset.All };
            bloodmage.StatsUnitClassification = new[] { UnitClassification.Ancient, UnitClassification.Giant };
            bloodmage.CombatTargetedAs = new[] { Target.Alive, Target.Hero };

            var workshop = new Unit(UnitType.Workshop, db);
            workshop.TechtreeResearchesAvailable = new[] { new Upgrade(UpgradeType.HumanFlare), new Upgrade(UpgradeType.HumanFragShards) };
            workshop.PathingPlacementRequires = new[] { PathingType.Unfloat, PathingType.Blighted };
            workshop.PathingPlacementPreventedBy = new[] { PathingType.Unbuildable, PathingType.Unwalkable };

            var expectUnit = new Dictionary<string, Type>()
            {
                // Bloodmage
                { "uani", bloodmage.ArtRequiredAnimationNamesRaw.GetType() },
                { "ubui", bloodmage.TechtreeStructuresBuiltRaw.GetType() },
                { "usei", bloodmage.TechtreeItemsSoldRaw.GetType() },
                { "ureq", bloodmage.TechtreeRequirementsRaw.GetType() },
                { "urqa", bloodmage.TechtreeRequirementsLevelsRaw.GetType() },
                { "uspa", bloodmage.ArtSpecialRaw.GetType() },
                { "uabi", bloodmage.AbilitiesNormalRaw.GetType() },
                { "uhab", bloodmage.AbilitiesHeroRaw.GetType() },
                { "util", bloodmage.EditorTilesetsRaw.GetType() },
                { "utyp", bloodmage.StatsUnitClassificationRaw.GetType() },
                { "utar", bloodmage.CombatTargetedAsRaw.GetType() },

                // Workshop
                { "ures", workshop.TechtreeResearchesAvailableRaw.GetType() },
                { "upap", workshop.PathingPlacementRequiresRaw.GetType() },
                { "upar", workshop.PathingPlacementPreventedByRaw.GetType() },

                // Arcane vault
                { "umki", arcaneVault.TechtreeItemsMadeRaw.GetType() },
            };

            foreach (var unit in objectData.GetAllUnits())
            {
                var actualUnit = db.GetUnit(unit.OldId);
                foreach (var mod in unit.Modifications)
                {
                    if (expectUnit.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != ObjectDataType.String)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualUnit.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            // Verify ability data
            var abilityData = objectData.AbilityData;

            // Empty lists test
            var aerialShackles = new Abilities.AerialShackles(db);
            aerialShackles.StatsBuffs[1] = Array.Empty<Buff>();
            aerialShackles.ArtLightningEffects = Array.Empty<LightningEffect>();

            var reveal = new Abilities.RevealArcaneTower(db);
            reveal.StatsEffects[1] = Array.Empty<Buff>();

            // Non-empty lists test
            var stormHammers = new Abilities.StormHammers(db);
            stormHammers.StatsBuffs[1] = new[] { new Buff(BuffType.AcidBomb), new Buff(BuffType.AntiMagicShell) };
            stormHammers.StatsEffects[1] = new[] { new Buff(BuffType.Blizzard_XHbz), new Buff(BuffType.CloudOfFog_Xclf) };
            stormHammers.ArtLightningEffects = new[] { LightningEffect.AFOD, LightningEffect.SPLK };

            var expectAbility = new Dictionary<string, Type>()
            {
                { "abuf", stormHammers.StatsBuffsRaw[1].GetType() },
                { "aeff", stormHammers.StatsEffectsRaw[1].GetType() },
                { "alig", stormHammers.ArtLightningEffectsRaw.GetType() },
            };

            foreach (var ability in objectData.GetAllAbilities())
            {
                var actualAbility = db.GetAbility(ability.OldId) ?? throw new NullReferenceException(ability.OldId.ToRawcode());
                foreach (var mod in ability.Modifications)
                {
                    if (expectAbility.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != ObjectDataType.String)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = mod.Level > 0 ? actualAbility.Modifications[mod.Id, mod.Level] : actualAbility.Modifications[mod.Id];
                        var value = actualMod.Value;
                        if (!object.Equals(mod.Value, value))
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' VALUE: Expected {{{mod.Value}}}, actual {{{value}}}.");
                            Assert.AreNotEqual(mod.Value.ToString(), value.ToString());
                        }
                    }
                    else
                    {
                        Assert.Fail($"Key '{mod.Id.ToRawcode()}' not found.");
                    }
                }
            }

            Assert.AreEqual(0, errorLines.Count, string.Join("\r\n  ", errorLines.Prepend("\r\nFound one or more errors:")));
        }
    }
}