namespace War3Api.Object
{
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

    public enum TechEquivalent
    {
        AnyAltar = ('T' << 0) | ('A' << 8) | ('L' << 16) | ('T' << 24),
        AnyHero = ('H' << 0) | ('E' << 8) | ('R' << 16) | ('O' << 24),
        AnyTier1Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('1' << 24),
        AnyTier2Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('2' << 24),
        AnyTier3Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('3' << 24),
        AnyTier4Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('4' << 24),
        AnyTier5Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('5' << 24),
        AnyTier6Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('6' << 24),
        AnyTier7Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('7' << 24),
        AnyTier8Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('8' << 24),
        AnyTier9Hall = ('T' << 0) | ('W' << 8) | ('N' << 16) | ('9' << 24),
    }
}