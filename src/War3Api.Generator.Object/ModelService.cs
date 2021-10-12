// ------------------------------------------------------------------------------
// <copyright file="ModelService.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;

using Humanizer;

using Microsoft.CodeAnalysis.CSharp;

using War3Api.Generator.Object.Models;

using War3Net.Build.Object;

namespace War3Api.Generator.Object
{
    internal static class ModelService
    {
        internal static IEnumerable<TypeModel> GetTypeModels()
        {
            // Basic types
            yield return GenerateTypeModel(ObjectDataType.Int);
            yield return GenerateTypeModel(ObjectDataType.Real);
            yield return GenerateTypeModel(ObjectDataType.Unreal);
            yield return GenerateTypeModel(ObjectDataType.String);
            yield return GenerateTypeModel(ObjectDataType.Bool);
            yield return GenerateTypeModel(ObjectDataType.Char);

            // Enums
            yield return GenerateTypeModel("aiBuffer", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("armorType", TypeModelCategory.EnumString);
            yield return GenerateTypeModel("attackBits", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("attackType", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("attributeType", TypeModelCategory.EnumString);
            yield return GenerateTypeModel("channelFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("channelType", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("combatSound", TypeModelCategory.EnumString);
            yield return GenerateTypeModel("deathType", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("defenseType", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("defenseTypeInt", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("destructableCategory", TypeModelCategory.EnumChar);
            yield return GenerateTypeModel("detectionType", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("doodadCategory", TypeModelCategory.EnumChar);
            yield return GenerateTypeModel("fullFlags", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("interactionFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("itemClass", TypeModelCategory.EnumString);
            yield return GenerateTypeModel("lightningEffect", TypeModelCategory.EnumString);
            yield return GenerateTypeModel("morphFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("moveType", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("pickFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("regenType", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("shadowImage", TypeModelCategory.EnumString);
            yield return GenerateTypeModel("silenceFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("spellDetail", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("stackFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("teamColor", TypeModelCategory.EnumInt);
            yield return GenerateTypeModel("unitRace", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("upgradeClass", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("upgradeEffect", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("versionFlags", TypeModelCategory.EnumFlags);
            yield return GenerateTypeModel("weaponType", TypeModelCategory.EnumLowercase);

            // Enums (only used for lists)
            yield return GenerateTypeModel("pathingPrevent", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("pathingRequire", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("target", TypeModelCategory.EnumLowercase);
            yield return GenerateTypeModel("tileset", TypeModelCategory.EnumChar);
            yield return GenerateTypeModel("unitClassification", TypeModelCategory.EnumLowercase);

            // Unused enums
            // TargetType.cs
            // TechAvail.cs

            // Strings
            yield return GenerateTypeModel("icon");
            yield return GenerateTypeModel("model");
            yield return GenerateTypeModel("orderString");
            yield return GenerateTypeModel("pathingTexture");
            yield return GenerateTypeModel("shadowTexture");
            yield return GenerateTypeModel("soundLabel");
            yield return GenerateTypeModel("texture");
            yield return GenerateTypeModel("uberSplat");
            yield return GenerateTypeModel("unitSound");

            // Objects
            yield return GenerateTypeModel("abilCode", "Ability");
            yield return GenerateTypeModel("unitCode", "Unit");
            yield return GenerateTypeModel("upgradeCode", "Upgrade");

            // Objects (only used for lists)
            yield return GenerateTypeModel("buffCode", "Buff");
            yield return GenerateTypeModel("itemCode", "Item");
            yield return GenerateTypeModel("techCode", "Tech");

            // Lists
            yield return GenerateTypeListModel("abilityList", "Ability"); // TODO: validate abilities are all unit/item abil
            yield return GenerateTypeListModel("abilitySkinList", "Ability");
            yield return GenerateTypeListModel("buffList", "Buff"); // TODO: validate buffs are all buff (rawcode starts with B, with some exceptions where it starts with A)
            yield return GenerateTypeListModel("effectList", "Buff"); // TODO: validate buffs are all effect (rawcode starts with X)
            yield return GenerateTypeListModel("heroAbilityList", "Ability"); // TODO: validate abilities are all hero abil
            yield return GenerateTypeListModel("intList", GetKeywordText(SyntaxKind.IntKeyword));
            yield return GenerateTypeListModel("itemList", "Item");
            yield return GenerateTypeListModel("lightningList", "LightningEffect");
            yield return GenerateTypeListModel("modelList", GetKeywordText(SyntaxKind.StringKeyword));
            yield return GenerateTypeListModel("pathingListPrevent", "PathingPrevent");
            yield return GenerateTypeListModel("pathingListRequire", "PathingRequire");
            yield return GenerateTypeListModel("stringList", GetKeywordText(SyntaxKind.StringKeyword));
            yield return GenerateTypeListModel("targetList", "Target");
            yield return GenerateTypeListModel("techList", "Tech");
            yield return GenerateTypeListModel("tilesetList", "Tileset");
            yield return GenerateTypeListModel("unitClass", "UnitClassification");
            yield return GenerateTypeListModel("unitList", "Unit");
            yield return GenerateTypeListModel("unitSkinList", "Unit");
            yield return GenerateTypeListModel("upgradeList", "Upgrade");
        }

        internal static IEnumerable<DataTypeModel> GetDataTypeModels()
        {
            yield return GenerateDataTypeModel(ObjectDataType.Int, nameof(ObjectDataModification.ValueAsInt));
            yield return GenerateDataTypeModel(ObjectDataType.Real, nameof(ObjectDataModification.ValueAsFloat));
            yield return GenerateDataTypeModel(ObjectDataType.Unreal, nameof(ObjectDataModification.ValueAsFloat));
            yield return GenerateDataTypeModel(ObjectDataType.String, nameof(ObjectDataModification.ValueAsString));
            yield return GenerateDataTypeModel(ObjectDataType.Bool, nameof(ObjectDataModification.ValueAsBool));
            yield return GenerateDataTypeModel(ObjectDataType.Char, nameof(ObjectDataModification.ValueAsChar));
        }

        private static TypeModel GenerateTypeModel(ObjectDataType type)
        {
            var model = new TypeModel();

            model.Name = type.ToString().ToLower();
            model.Type = type;
            model.Category = TypeModelCategory.Basic;
            model.Identifier = GetKeywordText(GetDataTypeKeyword(type));

            return model;
        }

        private static TypeModel GenerateTypeModel(string enumName, TypeModelCategory enumCategory)
        {
            var model = new TypeModel();

            model.Name = enumName;
            model.Type = enumCategory == TypeModelCategory.EnumInt || enumCategory == TypeModelCategory.EnumFlags ? ObjectDataType.Int : ObjectDataType.String;
            model.Category = enumCategory;
            model.Identifier = enumName.Dehumanize();

            return model;
        }

        private static TypeModel GenerateTypeModel(string name)
        {
            var model = new TypeModel();

            model.Name = name;
            model.Type = ObjectDataType.String;
            model.Category = TypeModelCategory.String;
            model.Identifier = GetKeywordText(SyntaxKind.StringKeyword);

            return model;
        }

        private static TypeModel GenerateTypeModel(string name, string className)
        {
            var model = new TypeModel();

            model.Name = name;
            model.Type = ObjectDataType.String;
            model.Category = TypeModelCategory.Object;
            model.Identifier = className;

            return model;
        }

        private static TypeModel GenerateTypeListModel(string listName, string listItemName)
        {
            var model = new TypeModel();

            model.Name = listName;
            model.Type = ObjectDataType.String;
            model.Category = TypeModelCategory.List;
            model.Identifier = listItemName;

            return model;
        }

        private static DataTypeModel GenerateDataTypeModel(ObjectDataType type, string propertyName)
        {
            var model = new DataTypeModel();

            model.Type = type;
            model.Identifier = GetKeywordText(GetDataTypeKeyword(type));
            model.PropertyName = propertyName;

            return model;
        }

        private static SyntaxKind GetDataTypeKeyword(ObjectDataType objectDataType)
        {
            return objectDataType switch
            {
                ObjectDataType.Int => SyntaxKind.IntKeyword,
                ObjectDataType.Real => SyntaxKind.FloatKeyword,
                ObjectDataType.Unreal => SyntaxKind.FloatKeyword,
                ObjectDataType.String => SyntaxKind.StringKeyword,
                ObjectDataType.Bool => SyntaxKind.BoolKeyword,
                ObjectDataType.Char => SyntaxKind.CharKeyword,
            };
        }

        // TODO: move to different class
        internal static string GetKeywordText(SyntaxKind syntaxKind)
        {
            return SyntaxFactory.Token(syntaxKind).ValueText;
        }
    }
}