namespace War3Api.Object
{
    public class Tech
    {
        public int Key { get; set; }

        public Unit AsUnit => ObjectDatabase.DefaultDatabase.GetUnit(Key);

        public Upgrade AsUpgrade => ObjectDatabase.DefaultDatabase.GetUpgrade(Key);

        public TechEquivalent AsTechEquivalent => (TechEquivalent)Key;
    }

    public enum Tileset
    {
        // None = '_',
        All = '*',
        Ashenvale = 'A',
        Barrens = 'B',
        BlackCitadel = 'K',
        Cityscape = 'Y',
        Dalaran = 'X',
        DalaranRuins = 'J',
        Dungeon = 'D',
        Felwood = 'C',
        IcecrownGlacier = 'I',
        LordaeronFall = 'F',
        LordaeronSummer = 'L',
        LordaeronWinter = 'W',
        Northrend = 'N',
        Outland = 'O',
        SunkenRuins = 'Z',
        Underground = 'G',
        Village = 'V',
        VillageFall = 'Q',
    }

    public enum DoodadCategory
    {
        Props = 0,
        Structures = 'S',
        Water = 'W',
        CliffTerrain = 'C',
        Environment = 'E',
        Cinematic = 'Z',
    }

    public enum DestructableCategory
    {
        TreesDestructibles = 'D',
        PathingBlockers = 'P',
        BridgesRamps = 'B',
    }

    public enum TechEquivalent
    {
        // TODO
    }
}