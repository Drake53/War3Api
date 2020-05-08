using System;

using War3Net.Common.Extensions;

namespace War3Api.Object
{
    public class Tech
    {
        private readonly ObjectDatabase _db;
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

        public Tech(ObjectDatabase db, int key)
        {
            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            if (Enum.IsDefined(typeof(TechEquivalent), key))
            {
                throw new ArgumentException($"Tech '{key.ToRawcode()}' is a {nameof(TechEquivalent)}.", nameof(key));
            }

            var baseObject = db.GetObject(key);
            if (baseObject != null && !(baseObject is Unit || baseObject is Upgrade))
            {
                throw new ArgumentException($"Tech object must be of type {nameof(Unit)} or {nameof(Upgrade)}.", nameof(key));
            }

            _db = db;
            _key = key;
        }

        public ObjectDatabase Db => _db;

        public int Key => _key;

        public Unit AsUnit => _db?.GetUnit(Key);

        public Upgrade AsUpgrade => _db?.GetUpgrade(Key);

        public TechEquivalent AsTechEquivalent => (TechEquivalent)Key;
    }
}