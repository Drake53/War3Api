namespace War3Api.Object
{
    public class Tech
    {
        public int Key { get; set; }

        public Unit AsUnit => ObjectDatabase.DefaultDatabase.GetUnit(Key);

        public Upgrade AsUpgrade => ObjectDatabase.DefaultDatabase.GetUpgrade(Key);

        public TechEquivalent AsTechEquivalent => (TechEquivalent)Key;
    }
}