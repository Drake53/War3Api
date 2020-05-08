namespace War3Api.Object
{
    public class Tech
    {
        private readonly ObjectDatabase _db;
        private readonly int _key;

        public Tech(Unit unit) { _db = unit.Db; _key = unit.Key; }
        
        public Tech(Upgrade upgrade) { _db = upgrade.Db; _key = upgrade.Key; }
        
        public Tech(TechEquivalent techEquivalent) { _db = null; _key = (int)techEquivalent; }

        internal Tech(ObjectDatabase db, int key) { _db = db; _key = key; }

        public ObjectDatabase Db => _db;

        public int Key => _key;

        public Unit AsUnit => _db?.GetUnit(Key);

        public Upgrade AsUpgrade => _db?.GetUpgrade(Key);

        public TechEquivalent AsTechEquivalent => (TechEquivalent)Key;
    }
}