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

using War3Net.Build.Object;
using War3Net.Common.Extensions;

namespace War3Api.Object.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDefaultDatabaseSingleton()
        {
            var u = new Unit(UnitType.Peasant);
            Assert.AreEqual(u.Db, ObjectDatabase.DefaultDatabase);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var db = new ObjectDatabase(Array.Empty<int>());

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
            var db = new ObjectDatabase(Array.Empty<int>());

            const string TestFilePath = @"..\..\..\TestData\TestEnums.w3o";
            var objectData = MapObjectData.Parse(File.OpenRead(TestFilePath));

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
            altar.PathingPlacementPreventedBy = new[] { PathingRequire.Unbuildable, PathingRequire.Blighted };
            altar.PathingPlacementRequires = Array.Empty<PathingPrevent>();
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

            foreach (var unit in unitData.GetData())
            {
                var actualUnit = db.GetUnit(unit.OldId).ObjectModification;
                foreach (var mod in unit)
                {
                    if (expectUnit.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualUnit[mod.Id];
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

            foreach (var item in itemData.GetData())
            {
                var actualItem = db.GetItem(item.OldId).ObjectModification;
                foreach (var mod in item)
                {
                    if (expectItem.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualItem[mod.Id];
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

            foreach (var destructable in destructableData.GetData())
            {
                var actualDestructable = db.GetDestructable(destructable.OldId).ObjectModification;
                foreach (var mod in destructable)
                {
                    if (expectDestructable.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualDestructable[mod.Id];
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

            foreach (var doodad in doodadData.GetData())
            {
                var actualDoodad = db.GetDoodad(doodad.OldId).ObjectModification;
                foreach (var mod in doodad)
                {
                    if (expectDoodad.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualDoodad[mod.Id];
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

            foreach (var ability in abilityData.GetData())
            {
                var actualAbility = db.GetAbility(ability.OldId)?.ObjectModification ?? throw new NullReferenceException(ability.OldId.ToRawcode());
                foreach (var mod in ability)
                {
                    if (expectAbility.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = mod.Level.HasValue ? actualAbility[mod.Id, mod.Level.Value] : actualAbility[mod.Id];
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

            foreach (var buff in buffData.GetData())
            {
                var actualBuff = db.GetBuff(buff.OldId).ObjectModification;
                foreach (var mod in buff)
                {
                    if (expectBuff.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualBuff[mod.Id];
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

            foreach (var upgrade in upgradeData.GetData())
            {
                var actualUpgrade = db.GetUpgrade(upgrade.OldId).ObjectModification;
                foreach (var mod in upgrade)
                {
                    if (expectUpgrade.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != type)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualUpgrade[mod.Id];
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
            var db = new ObjectDatabase(Array.Empty<int>());

            const string TestFilePath = @"..\..\..\TestData\TestLists.w3o";
            var objectData = MapObjectData.Parse(File.OpenRead(TestFilePath));

            var errorLines = new List<string>();

            // Verify unit data
            var unitData = objectData.UnitData;

            // Empty lists test
            var peasant = new Unit(UnitType.Peasant, db);
            peasant.TechtreeStructuresBuilt = Array.Empty<Unit>();

            var keep = new Unit(UnitType.Keep, db);
            keep.ArtRequiredAnimationNames = Array.Empty<string>();
            keep.PathingPlacementRequires = Array.Empty<PathingPrevent>();
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
            ziggurat.PathingPlacementPreventedBy = Array.Empty<PathingRequire>();

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
            workshop.PathingPlacementRequires = new[] { PathingPrevent.Unfloat, PathingPrevent.Blighted };
            workshop.PathingPlacementPreventedBy = new[] { PathingRequire.Unbuildable, PathingRequire.Unwalkable };

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

            foreach (var unit in unitData.GetData())
            {
                var actualUnit = db.GetUnit(unit.OldId).ObjectModification;
                foreach (var mod in unit)
                {
                    if (expectUnit.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != ObjectDataType.String)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = actualUnit[mod.Id];
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

            foreach (var ability in abilityData.GetData())
            {
                var actualAbility = db.GetAbility(ability.OldId)?.ObjectModification ?? throw new NullReferenceException(ability.OldId.ToRawcode());
                foreach (var mod in ability)
                {
                    if (expectAbility.TryGetValue(mod.Id.ToRawcode(), out var type))
                    {
                        if (mod.Type != ObjectDataType.String)
                        {
                            errorLines.Add($"'{mod.Id.ToRawcode()}' TYPE: Expected {mod.Type}, actual {type}.");
                        }

                        var actualMod = mod.Level.HasValue ? actualAbility[mod.Id, mod.Level.Value] : actualAbility[mod.Id];
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