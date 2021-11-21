using System;

namespace War3Api.Object
{
    public class Tech
    {
        private readonly ObjectDatabaseBase? _db;
        private readonly int _key;

        public Tech(Unit unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            _db = unit.Db;
            _key = unit.Key;
        }

        public Tech(Upgrade upgrade)
        {
            if (upgrade is null)
            {
                throw new ArgumentNullException(nameof(upgrade));
            }

            _db = upgrade.Db;
            _key = upgrade.Key;
        }

        public Tech(TechEquivalent techEquivalent)
        {
            if (!Enum.IsDefined(typeof(TechEquivalent), techEquivalent))
            {
                throw new ArgumentOutOfRangeException(nameof(techEquivalent));
            }

            _db = null;
            _key = (int)techEquivalent;
        }

        public Tech(int key)
            : this(ObjectDatabase.DefaultDatabase, key)
        {
        }

        public Tech(ObjectDatabaseBase db, int key)
        {
            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            try
            {
                db.ReserveTech(key);
            }
            catch (ArgumentException)
            {
                throw;
            }

            _db = db;
            _key = key;
        }

        public ObjectDatabaseBase? Db => _db;

        public int Key => _key;

        public Unit AsUnit => _db?.GetUnit(Key);

        public Upgrade AsUpgrade => _db?.GetUpgrade(Key);

        public TechEquivalent AsTechEquivalent => (TechEquivalent)Key;
    }
}