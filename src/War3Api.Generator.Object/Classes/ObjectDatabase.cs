#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using War3Net.Build.Object;
using War3Net.Common.Extensions;

namespace War3Api.Object
{
    public sealed class ObjectDatabase
    {
        private static readonly Lazy<ObjectDatabase> _defaultDatabase = new Lazy<ObjectDatabase>();
        private static readonly Lazy<HashSet<int>> _objectTypes = new Lazy<HashSet<int>>(() => GetObjectTypes().ToHashSet());
        private static readonly Lazy<HashSet<int>> _techTypes = new Lazy<HashSet<int>>(() => GetTechTypes().ToHashSet());

        private readonly HashSet<int> _reservedKeys;
        private readonly HashSet<int> _reservedTechs;
        private readonly Dictionary<int, BaseObject> _objects;

        public ObjectDatabase()
            : this(GetDefaultReservedKeys(), Array.Empty<BaseObject>())
        {
        }

        public ObjectDatabase(IEnumerable<int> reservedKeys)
            : this(reservedKeys, Array.Empty<BaseObject>())
        {
        }

        public ObjectDatabase(IEnumerable<BaseObject> objects)
            : this(GetDefaultReservedKeys(), objects)
        {
        }

        public ObjectDatabase(IEnumerable<int> reservedKeys, IEnumerable<BaseObject> objects)
        {
            _reservedKeys = reservedKeys.ToHashSet();
            _reservedTechs = new HashSet<int>();
            _objects = objects.ToDictionary(obj => obj.Key);
        }

        public static ObjectDatabase DefaultDatabase => _defaultDatabase.Value;

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
            return _objects[id];
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
            if (_objects.TryGetValue(id, out var baseObject))
            {
                return baseObject;
            }

            if (_objectTypes.Value.Contains(id))
            {
                // todo: create new?
            }

            return null;
        }

        public Tech? TryGetTech(int id)
        {
            if (Enum.IsDefined(typeof(TechEquivalent), id))
            {
                return new Tech((TechEquivalent)id);
            }

            var baseObject = TryGetObject(id);
            if (baseObject != null && (baseObject is Unit || baseObject is Upgrade))
            {
                return new Tech(this, id);
            }

            return null;
        }

        public MapObjectData GetAllData()
        {
            var units = _objects.Where(pair => pair.Value is Unit);
            var items = _objects.Where(pair => pair.Value is Item);
            var destructables = _objects.Where(pair => pair.Value is Destructable);
            var doodads = _objects.Where(pair => pair.Value is Doodad);
            var abilities = _objects.Where(pair => pair.Value is Ability);
            var buffs = _objects.Where(pair => pair.Value is Buff);
            var upgrades = _objects.Where(pair => pair.Value is Upgrade);

            return new MapObjectData(
                units.Any() ? new MapUnitObjectData(units.Select(pair => pair.Value.ObjectModification).ToArray()) : null,
                items.Any() ? new MapItemObjectData(items.Select(pair => pair.Value.ObjectModification).ToArray()) : null,
                destructables.Any() ? new MapDestructableObjectData(destructables.Select(pair => pair.Value.ObjectModification).ToArray()) : null,
                doodads.Any() ? new MapDoodadObjectData(doodads.Select(pair => pair.Value.ObjectModification).ToArray()) : null,
                abilities.Any() ? new MapAbilityObjectData(abilities.Select(pair => pair.Value.ObjectModification).ToArray()) : null,
                buffs.Any() ? new MapBuffObjectData(buffs.Select(pair => pair.Value.ObjectModification).ToArray()) : null,
                upgrades.Any() ? new MapUpgradeObjectData(upgrades.Select(pair => pair.Value.ObjectModification).ToArray()) : null);
        }

        internal void AddObject(BaseObject baseObject)
        {
            if (_objects.ContainsKey(baseObject.Key))
            {
                throw new ArgumentException($"An object with key '{baseObject.Key.ToRawcode()}' has already been added to this database.");
            }

            if (!_objectTypes.Value.Contains(baseObject.ObjectModification.OldId))
            {
                throw new ArgumentOutOfRangeException($"Base object key '{baseObject.ObjectModification.OldId.ToRawcode()}' is not valid.");
            }

            if (baseObject.ObjectModification.NewId != 0)
            {
                if (_reservedKeys.Contains(baseObject.Key))
                {
                    throw new ArgumentException($"Key '{baseObject.Key.ToRawcode()}' is reserved in this database.");
                }

                if (_reservedTechs.Contains(baseObject.Key))
                {
                    if (!(baseObject is Unit || baseObject is Upgrade))
                    {
                        throw new ArgumentException($"Key '{baseObject.Key.ToRawcode()}' is reserved for a {nameof(Tech)} object, which must be of type {nameof(Unit)} or {nameof(Upgrade)}.");
                    }

                    _reservedTechs.Remove(baseObject.Key);
                }
            }

            _objects.Add(baseObject.Key, baseObject);
        }

        internal void ReserveTech(int id)
        {
            if (_techTypes.Value.Contains(id))
            {
                return;
            }

            if (_objects.TryGetValue(id, out var baseObject))
            {
                if (baseObject is Unit || baseObject is Upgrade)
                {
                    return;
                }

                throw new ArgumentException($"Cannot reserve key '{id.ToRawcode()}' as a {nameof(Tech)} object, because an object with this key has already been added to this database.");
            }

            if (_objectTypes.Value.Contains(id))
            {
                throw new ArgumentException($"Cannot reserve key '{id.ToRawcode()}' as a {nameof(Tech)} object, because it is already reserved for an object that is neither of type {nameof(Unit)} nor {nameof(Upgrade)}.");
            }

            if (_reservedKeys.Contains(id))
            {
                throw new ArgumentException($"Cannot reserve key '{id.ToRawcode()}' as a {nameof(Tech)} object, because this key is reserved in this database.");
            }

            _reservedTechs.Add(id);
        }

        private static IEnumerable<int> GetObjectTypes()
        {
            foreach (var unitType in Enum.GetValues(typeof(UnitType)))
            {
                yield return (int)unitType;
            }

            foreach (var itemType in Enum.GetValues(typeof(ItemType)))
            {
                yield return (int)itemType;
            }

            foreach (var destructableType in Enum.GetValues(typeof(DestructableType)))
            {
                yield return (int)destructableType;
            }

            foreach (var doodadType in Enum.GetValues(typeof(DoodadType)))
            {
                yield return (int)doodadType;
            }

            foreach (var abilityType in Enum.GetValues(typeof(AbilityType)))
            {
                yield return (int)abilityType;
            }

            foreach (var buffType in Enum.GetValues(typeof(BuffType)))
            {
                yield return (int)buffType;
            }

            foreach (var upgradeType in Enum.GetValues(typeof(UpgradeType)))
            {
                yield return (int)upgradeType;
            }
        }

        private static IEnumerable<int> GetTechTypes()
        {
            foreach (var unitType in Enum.GetValues(typeof(UnitType)))
            {
                yield return (int)unitType;
            }

            foreach (var upgradeType in Enum.GetValues(typeof(UpgradeType)))
            {
                yield return (int)upgradeType;
            }
        }

        private static IEnumerable<int> GetDefaultReservedKeys()
        {
            foreach (var techEquivalent in Enum.GetValues(typeof(TechEquivalent)))
            {
                yield return (int)techEquivalent;
            }

            // todo: set default reserved keys (random unit/item, etc)
        }
    }
}