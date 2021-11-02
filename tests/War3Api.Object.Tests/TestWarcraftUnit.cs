using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using War3Net.Build.Extensions;

namespace War3Api.Object.Tests
{
    //TestData contains an Archmage with the following modified stats:
    //Categorization - Campaign: True
    //Speed Base: 522
    //Agility per level: 2.5
    //Editor suffix: "Test"
    //Hotkey: "Z"
    //The data was generated using Warcraft 3 1.32.

    [TestClass]
    public class TestWarcraftUnit
    {
        private const string _testFile = @"..\..\..\TestData\TestArchmage.w3o";
        private ObjectDatabase _objectDatabase;
        private IEnumerable<Unit> _units;
        private Unit _archmage;

        [TestInitialize]
        public void Initialize()
        {
            using var fileStream = File.OpenRead(_testFile);
            using var binaryReader = new BinaryReader(fileStream);
            var objectData = binaryReader.ReadObjectData(false);

            var objectDatabase = new ObjectDatabase();
            objectDatabase.AddObjects(objectData);
            _objectDatabase = objectDatabase;

            _units = objectDatabase.GetUnits();
            _archmage = _units.First();
        }

        [TestMethod]
        public void WarcraftUnit_CategorizationCampaignx_True()
        {
            _ = _archmage.EditorCategorizationCampaign == true;
        }

        [TestMethod]
        public void WarcraftUnit_GetSpeedBase_522()
        {
            _ = _archmage.MovementSpeedBase == 522;
        }

        [TestMethod]
        public void WarcraftUnit_GetAgilityPerLevel_2Point5()
        {
            _ = _archmage.StatsAgilityPerLevel == 2.5;
        }

        [TestMethod]
        public void WarcraftUnit_GetEditorSuffix_IsTest()
        {
            _ = _archmage.TextNameEditorSuffix == "Test";
        }

        [TestMethod]
        public void WarcraftUnit_Hotkey_Z()
        {
            _ = _archmage.TextHotkey == 'Z';
        }
    }
}
