using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using War3Api.Object.Enums;

using War3Net.Common.Extensions;

namespace War3Api.Object
{
    public abstract class ObjectDatabaseBase
    {
        internal static readonly Lazy<HashSet<int>> _objectTypes = new(() => GetObjectTypes().ToHashSet());

        private readonly ObjectDatabaseBase? _fallbackDatabase;

        internal ObjectDatabaseBase(ObjectDatabaseBase? fallbackDatabase)
        {
            _fallbackDatabase = fallbackDatabase;
        }

        internal ObjectDatabaseBase? FallbackDatabase => _fallbackDatabase;

        public Unit GetUnit(int id)
        {
            return (Unit)GetObject(id);
        }

        public Item GetItem(int id)
        {
            return (Item)GetObject(id);
        }

        public Destructable GetDestructable(int id)
        {
            return (Destructable)GetObject(id);
        }

        public Doodad GetDoodad(int id)
        {
            return (Doodad)GetObject(id);
        }

        public Ability GetAbility(int id)
        {
            return (Ability)GetObject(id);
        }

        public Buff GetBuff(int id)
        {
            return (Buff)GetObject(id);
        }

        public Upgrade GetUpgrade(int id)
        {
            return (Upgrade)GetObject(id);
        }

        public BaseObject GetObject(int id)
        {
            return TryGetObject(id, out var baseObject) ? baseObject : throw new KeyNotFoundException($"Could not find object with key '{id.ToRawcode()}' in the database.");
        }

        public Tech GetTech(int id)
        {
            return Enum.IsDefined(typeof(TechEquivalent), id) ? new Tech((TechEquivalent)id) : new Tech(this, id);
        }

        public Unit? TryGetUnit(int id)
        {
            return TryGetObject(id) as Unit;
        }

        public Item? TryGetItem(int id)
        {
            return TryGetObject(id) as Item;
        }

        public Destructable? TryGetDestructable(int id)
        {
            return TryGetObject(id) as Destructable;
        }

        public Doodad? TryGetDoodad(int id)
        {
            return TryGetObject(id) as Doodad;
        }

        public Ability? TryGetAbility(int id)
        {
            return TryGetObject(id) as Ability;
        }

        public Buff? TryGetBuff(int id)
        {
            return TryGetObject(id) as Buff;
        }

        public Upgrade? TryGetUpgrade(int id)
        {
            return TryGetObject(id) as Upgrade;
        }

        public BaseObject? TryGetObject(int id)
        {
            return TryGetObject(id, out var baseObject) ? baseObject : null;
        }

        public Tech? TryGetTech(int id)
        {
            return TryGetTech(id, out var tech) ? tech : null;
        }

        public bool TryGetUnit(int id, [NotNullWhen(true)] out Unit? unit)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Unit unit_)
            {
                unit = unit_;
                return true;
            }

            unit = null;
            return false;
        }

        public bool TryGetItem(int id, [NotNullWhen(true)] out Item? item)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Item item_)
            {
                item = item_;
                return true;
            }

            item = null;
            return false;
        }

        public bool TryGetDestructable(int id, [NotNullWhen(true)] out Destructable? destructable)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Destructable destructable_)
            {
                destructable = destructable_;
                return true;
            }

            destructable = null;
            return false;
        }

        public bool TryGetDoodad(int id, [NotNullWhen(true)] out Doodad? doodad)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Doodad doodad_)
            {
                doodad = doodad_;
                return true;
            }

            doodad = null;
            return false;
        }

        public bool TryGetAbility(int id, [NotNullWhen(true)] out Ability? ability)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Ability ability_)
            {
                ability = ability_;
                return true;
            }

            ability = null;
            return false;
        }

        public bool TryGetBuff(int id, [NotNullWhen(true)] out Buff? buff)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Buff buff_)
            {
                buff = buff_;
                return true;
            }

            buff = null;
            return false;
        }

        public bool TryGetUpgrade(int id, [NotNullWhen(true)] out Upgrade? upgrade)
        {
            if (TryGetObject(id, out var baseObject) && baseObject is Upgrade upgrade_)
            {
                upgrade = upgrade_;
                return true;
            }

            upgrade = null;
            return false;
        }

        public bool TryGetTech(int id, [NotNullWhen(true)] out Tech? tech)
        {
            if (Enum.IsDefined(typeof(TechEquivalent), id))
            {
                tech = new Tech((TechEquivalent)id);
                return true;
            }

            if (TryGetObject(id, out var baseObject) && (baseObject is Unit || baseObject is Upgrade))
            {
                tech = new Tech(this, id);
                return true;
            }

            tech = null;
            return false;
        }

        public abstract bool TryGetObject(int id, [NotNullWhen(true)] out BaseObject? baseObject);

        internal abstract void AddObject(BaseObject baseObject);

        internal abstract void ReserveTech(int id);

        private static IEnumerable<int> GetObjectTypes()
        {
            foreach (var unitType in Enum.GetValues<UnitType>())
            {
                yield return (int)unitType;
            }

            foreach (var itemType in Enum.GetValues<ItemType>())
            {
                yield return (int)itemType;
            }

            foreach (var destructableType in Enum.GetValues<DestructableType>())
            {
                yield return (int)destructableType;
            }

            foreach (var doodadType in Enum.GetValues<DoodadType>())
            {
                yield return (int)doodadType;
            }

            foreach (var abilityType in Enum.GetValues<AbilityType>())
            {
                yield return (int)abilityType;
            }

            foreach (var buffType in Enum.GetValues<BuffType>())
            {
                yield return (int)buffType;
            }

            foreach (var upgradeType in Enum.GetValues<UpgradeType>())
            {
                yield return (int)upgradeType;
            }
        }
    }
}