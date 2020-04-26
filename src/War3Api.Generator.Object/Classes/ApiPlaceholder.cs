namespace War3Api.Object
{
    public class Tech
    {
        public int Key { get; set; }

        public Unit AsUnit => ObjectDatabase.DefaultDatabase.GetUnit(Key);

        public Upgrade AsUpgrade => ObjectDatabase.DefaultDatabase.GetUpgrade(Key);

        public TechEquivalent AsTechEquivalent => (TechEquivalent)Key;
    }

    /*public enum Tileset
    {
        // None = '_',
        All = '*',

        // has same members as War3Net.Build.Common.Tileset
    }*/

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