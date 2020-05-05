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

            peasant.AcquisitionRange = 123;
            Assert.AreEqual(123, peasant.AcquisitionRange);

            peasant.ArmorType = ArmorType.Metal;
            Assert.AreEqual(ArmorType.Metal.ToString(), peasant.ArmorTypeRaw);

            var abilities = new List<Ability>();
            // abilities.Add(new Ability(AbilityType.RepairHuman));
            abilities.Add(new Abilities.RepairHuman(db));
            var up = new Abilities.TankUpgrade(db);
            up.NewUnitType[1] = peasant;

            peasant.Normal = abilities;
            Assert.AreEqual(((int)AbilityType.RepairHuman).ToRawcode(), peasant.NormalRaw);
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
            peasant.ShadowImageUnit = ShadowImage.ShadowFlyer;
            peasant.ArmorType = ArmorType.Stone;
            peasant.Attack1AttackType = AttackType.Hero;
            peasant.AttacksEnabled = AttackBits.Attack2Only;
            peasant.AIPlacementType = AiBuffer.Resource;
            peasant.TeamColor = TeamColor.Player1Red;

            var paladin = new Unit(UnitType.Paladin, db);
            paladin.Attack2WeaponSound = CombatSound.MetalLightChop;
            paladin.DeathType = DeathType.CanRaiseDoesDecay;
            paladin.DefenseType = DefenseType.Normal;
            paladin.Type = MoveType.Amph;
            paladin.PrimaryAttribute = AttributeType.AGI;

            var altar = new Unit(UnitType.Altarofkings, db);
            altar.ModelFileExtraVersions = VersionFlags.ReignOfChaos | VersionFlags.TheFrozenThrone;
            altar.Attack1WeaponType = WeaponType.Mline;
            altar.PlacementPreventedBy = new[] { PathingRequire.Unbuildable, PathingRequire.Blighted };
            altar.PlacementRequires = Array.Empty<PathingPrevent>();
            altar.HitPointsRegenerationType = RegenType.Night;
            altar.Race = UnitRace.Other;
            altar.UnitClassification = new[] { UnitClassification.Ancient, UnitClassification.Mechanical, UnitClassification.Peon };

            var expectUnit = new Dictionary<string, ObjectDataType>()
            {
                // Peasant
                { "ushu", castType[peasant.ShadowImageUnitRaw.GetType()] },
                { "uarm", castType[peasant.ArmorTypeRaw.GetType()] },
                { "ua1t", castType[peasant.Attack1AttackTypeRaw.GetType()] },
                { "uaen", castType[peasant.AttacksEnabledRaw.GetType()] },
                { "uabt", castType[peasant.AIPlacementTypeRaw.GetType()] },
                { "utco", castType[peasant.TeamColorRaw.GetType()] },

                // Paladin
                { "ucs2", castType[paladin.Attack2WeaponSoundRaw.GetType()] },
                { "udea", castType[paladin.DeathTypeRaw.GetType()] },
                { "udty", castType[paladin.DefenseTypeRaw.GetType()] },
                { "umvt", castType[paladin.TypeRaw.GetType()] },
                { "upra", castType[paladin.PrimaryAttributeRaw.GetType()] },

                // Altar
                { "uver", castType[altar.ModelFileExtraVersionsRaw.GetType()] },
                { "ua1w", castType[altar.Attack1WeaponTypeRaw.GetType()] },
                { "upar", castType[altar.PlacementPreventedByRaw.GetType()] },
                { "upap", castType[altar.PlacementRequiresRaw.GetType()] },
                { "uhrt", castType[altar.HitPointsRegenerationTypeRaw.GetType()] },
                { "urac", castType[altar.RaceRaw.GetType()] },
                { "utyp", castType[altar.UnitClassificationRaw.GetType()] },
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
            gauntlets.Classification = ItemClass.Miscellaneous;

            var expectItem = new Dictionary<string, ObjectDataType>()
            {
                // Gauntlets
                { "icla", castType[gauntlets.ClassificationRaw.GetType()] },
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
            stoneDoor.Category = DestructableCategory.BridgesRamps;
            stoneDoor.Tilesets = new[] { Tileset.LordaeronFall, Tileset.LordaeronSummer,Tileset.SunkenRuins };

            var expectDestructable = new Dictionary<string, ObjectDataType>()
            {
                // Stone door
                { "bcat", castType[stoneDoor.CategoryRaw.GetType()] },
                { "btil", castType[stoneDoor.TilesetsRaw.GetType()] },
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
            trough.Category = DoodadCategory.Cinematic;

            var expectDoodad = new Dictionary<string, ObjectDataType>()
            {
                // Trough
                { "dcat", castType[trough.CategoryRaw.GetType()] },
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
            darkRangerSilence.AttacksPrevented[1] = SilenceFlags.Melee | SilenceFlags.Ranged | SilenceFlags.Special | SilenceFlags.Spells;

            var demonHunterMetamorphosis = new Abilities.DemonHunterMetamorphosis(db);
            demonHunterMetamorphosis.MorphingFlags[1] = MorphFlags.RequiresPayment;

            var farseerFarSight = new Abilities.FarseerFarSight(db);
            farseerFarSight.DetectionType[1] = DetectionType.Invisible;

            var seaWitchFrostArrows = new Abilities.SeaWitchFrostArrows(db);
            seaWitchFrostArrows.StackFlags[1] = 0;

            var illidanChannel = new Abilities.IllidanChannel(db);
            illidanChannel.TargetType[1] = ChannelType.UnitOrPointTarget;
            illidanChannel.Options[1] = ChannelFlags.UniversalSpell | ChannelFlags.UniqueCast;

            var alliedBuilding = new Abilities.AlliedBuilding(db);
            alliedBuilding.InteractionType[1] = InteractionFlags.AnyUnitWInventory | InteractionFlags.AnyNonBuilding;

            var rejuvination = new Abilities.Rejuvination(db);
            rejuvination.AllowWhenFull[1] = FullFlags.ManaOnly;

            var rootAncientProtector = new Abilities.RootAncientProtector(db);
            rootAncientProtector.UprootedDefenseType[1] = DefenseTypeInt.Normal;

            var preservation = new Abilities.Preservation(db);
            preservation.BuildingTypesAllowed[1] = PickFlags.General;

            var expectAbility = new Dictionary<string, ObjectDataType>()
            {
                // Dark ranger silence
                { "Nsi1", castType[darkRangerSilence.AttacksPreventedRaw[1].GetType()] },

                // Demon hunter metamorphosis
                { "Eme2", castType[demonHunterMetamorphosis.MorphingFlagsRaw[1].GetType()] },

                // Farseer far sight
                { "Ofs1", castType[farseerFarSight.DetectionTypeRaw[1].GetType()] },

                // Sea witch frost arrows
                { "Hca4", castType[seaWitchFrostArrows.StackFlagsRaw[1].GetType()] },

                // Illidan channel
                { "Ncl2", castType[illidanChannel.TargetTypeRaw[1].GetType()] },
                { "Ncl3", castType[illidanChannel.OptionsRaw[1].GetType()] },

                // Allied building
                { "Neu2", castType[alliedBuilding.InteractionTypeRaw[1].GetType()] },

                // Rejuvination
                { "Rej3", castType[rejuvination.AllowWhenFullRaw[1].GetType()] },

                // Root ancient protector
                { "Roo4", castType[rootAncientProtector.UprootedDefenseTypeRaw[1].GetType()] },

                // Preservation
                { "Npr1", castType[preservation.BuildingTypesAllowedRaw[1].GetType()] },
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
            avatar.Lightning = LightningEffect.DRAB;
            avatar.RequiredSpellDetail = SpellDetail.Medium;

            var expectBuff = new Dictionary<string, ObjectDataType>()
            {
                // Avatar
                { "flig", castType[avatar.LightningRaw.GetType()] },
                { "fspd", castType[avatar.RequiredSpellDetailRaw.GetType()] },
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
            humanAnimalBreeding.Class = UpgradeClass.Caster;
            humanAnimalBreeding.Effect2 = UpgradeEffect.Radl;

            var expectUpgrade = new Dictionary<string, ObjectDataType>()
            {
                // Human animal breeding
                { "gcls", castType[humanAnimalBreeding.ClassRaw.GetType()] },
                { "gef2", castType[humanAnimalBreeding.Effect2Raw.GetType()] },
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
            peasant.StructuresBuilt = Array.Empty<Unit>();

            var keep = new Unit(UnitType.Keep, db);
            keep.RequiredAnimationNames = Array.Empty<string>();
            keep.PlacementRequires = Array.Empty<PathingPrevent>();
            keep.ResearchesAvailable = Array.Empty<Upgrade>();
            keep.UnitClassification = Array.Empty<UnitClassification>();

            var cannonTower = new Unit(UnitType.Cannontower, db);
            cannonTower.Requirements = Array.Empty<Tech>();

            var arcaneVault = new Unit(UnitType.Arcanevault, db);
            arcaneVault.ItemsMade = Array.Empty<Item>();

            var paladin = new Unit(UnitType.Paladin, db);
            paladin.Special = Array.Empty<string>();
            paladin.Normal = Array.Empty<Ability>();
            paladin.Hero = Array.Empty<Ability>();
            paladin.Tilesets = Array.Empty<Tileset>();
            paladin.TargetedAs = Array.Empty<Target>();

            var ziggurat = new Unit(UnitType.Ziggurat, db);
            ziggurat.PlacementPreventedBy = Array.Empty<PathingRequire>();

            // Non-empty lists test
            var bloodmage = new Unit(UnitType.Bloodmage, db);
            bloodmage.RequiredAnimationNames = new[] { "upgrade", "second" };
            bloodmage.StructuresBuilt = new[] { keep, cannonTower, arcaneVault };
            bloodmage.ItemsSold = new[] { new Item(ItemType.ClawsOfAttack15), new Item(ItemType.CrownOfKings5) };
            bloodmage.Requirements = new[] { new Tech() { Key = peasant.Key }, new Tech() { Key = new Upgrade(UpgradeType.HumanAnimalBreeding).Key }, new Tech() { Key = "TWN1".FromRawcode() } };
            bloodmage.RequirementsLevels = new[] { 1, 2, 0 };
            bloodmage.Special = new[] { @"buildings\other\ElvenFarm\ElvenFarm.mdl", string.Empty };
            bloodmage.Normal = new Ability[] { new Abilities.Alarm(), new Abilities.Inventory() };
            bloodmage.Hero = new Ability[] { new Abilities.BloodMageFlameStrike(), new Abilities.BloodMageBanish() };
            bloodmage.Tilesets = new[] { Tileset.Ashenvale, Tileset.All };
            bloodmage.UnitClassification = new[] { UnitClassification.Ancient, UnitClassification.Giant };
            bloodmage.TargetedAs = new[] { Target.Alive, Target.Hero };

            var workshop = new Unit(UnitType.Workshop, db);
            workshop.ResearchesAvailable = new[] { new Upgrade(UpgradeType.HumanFlare), new Upgrade(UpgradeType.HumanFragShards) };
            workshop.PlacementRequires = new[] { PathingPrevent.Unfloat, PathingPrevent.Blighted };
            workshop.PlacementPreventedBy = new[] { PathingRequire.Unbuildable, PathingRequire.Unwalkable };

            var expectUnit = new Dictionary<string, Type>()
            {
                // Bloodmage
                { "uani", bloodmage.RequiredAnimationNames.GetType() },
                { "ubui", bloodmage.StructuresBuiltRaw.GetType() },
                { "usei", bloodmage.ItemsSoldRaw.GetType() },
                { "ureq", bloodmage.RequirementsRaw.GetType() },
                { "urqa", bloodmage.RequirementsLevelsRaw.GetType() },
                { "uspa", bloodmage.SpecialRaw.GetType() },
                { "uabi", bloodmage.NormalRaw.GetType() },
                { "uhab", bloodmage.HeroRaw.GetType() },
                { "util", bloodmage.TilesetsRaw.GetType() },
                { "utyp", bloodmage.UnitClassificationRaw.GetType() },
                { "utar", bloodmage.TargetedAsRaw.GetType() },

                // Workshop
                { "ures", workshop.ResearchesAvailableRaw.GetType() },
                { "upap", workshop.PlacementRequiresRaw.GetType() },
                { "upar", workshop.PlacementPreventedByRaw.GetType() },

                // Arcane vault
                { "umki", arcaneVault.ItemsMadeRaw.GetType() },
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
            aerialShackles.Buffs[1] = Array.Empty<Buff>();
            aerialShackles.LightningEffects = Array.Empty<LightningEffect>();

            var reveal = new Abilities.RevealArcaneTower(db);
            reveal.Effects[1] = Array.Empty<Buff>();

            // Non-empty lists test
            var stormHammers = new Abilities.StormHammers(db);
            stormHammers.Buffs[1] = new[] { new Buff(BuffType.AcidBomb), new Buff(BuffType.AntiMagicShell) };
            stormHammers.Effects[1] = new[] { new Buff(BuffType.Blizzard_XHbz), new Buff(BuffType.CloudOfFog_Xclf) };
            stormHammers.LightningEffects = new[] { LightningEffect.AFOD, LightningEffect.SPLK };

            var expectAbility = new Dictionary<string, Type>()
            {
                { "abuf", stormHammers.BuffsRaw[1].GetType() },
                { "aeff", stormHammers.EffectsRaw[1].GetType() },
                { "alig", stormHammers.LightningEffectsRaw.GetType() },
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