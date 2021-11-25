﻿// ------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;

namespace War3Api.Generator.Object
{
    internal static class Program
    {
        private const string Version = "1.31.1.12173";
        private const string InputFolder = @"..\..\..\API";
        private const string OutputFolder = @"..\..\..\..\War3Api.Object\Generated";

        private const string InputAbsolute = null;
        private const string OutputAbsolute = null;

        private static void Main(string[] args)
        {
            var inputFolder = InputAbsolute ?? Path.Combine(InputFolder, Version);
            var outputFolder = OutputAbsolute ?? Path.Combine(OutputFolder, Version);

            ObjectApiGenerator.InitializeGenerator(inputFolder, outputFolder);

            Parallel.Invoke(new Action[]
            {
                () => ObjectApiGenerator.GenerateDataConverter(),
                () => ObjectApiGenerator.GenerateEnums(),

                () => UnitApiGenerator.InitializeGenerator(inputFolder),
                () => ItemApiGenerator.InitializeGenerator(inputFolder),
                () => DestructableApiGenerator.InitializeGenerator(inputFolder),
                () => DoodadApiGenerator.InitializeGenerator(inputFolder),
                () => AbilityApiGenerator.InitializeGenerator(inputFolder),
                () => BuffApiGenerator.InitializeGenerator(inputFolder),
                () => UpgradeApiGenerator.InitializeGenerator(inputFolder),
            });

            Parallel.Invoke(new Action[]
            {
                () => UnitApiGenerator.Generate(),
                () => ItemApiGenerator.Generate(),
                () => DestructableApiGenerator.Generate(),
                () => DoodadApiGenerator.Generate(),
                () => AbilityApiGenerator.Generate(),
                () => BuffApiGenerator.Generate(),
                () => UpgradeApiGenerator.Generate(),
            });
        }
    }
}