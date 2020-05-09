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
            return GetObject(id) as Unit;
        }

        public Item GetItem(int id)
        {
            return GetObject(id) as Item;
        }

        public Destructable GetDestructable(int id)
        {
            return GetObject(id) as Destructable;
        }

        public Doodad GetDoodad(int id)
        {
            return GetObject(id) as Doodad;
        }

        public Ability GetAbility(int id)
        {
            return GetObject(id) as Ability;
        }

        public Buff GetBuff(int id)
        {
            return GetObject(id) as Buff;
        }

        public Upgrade GetUpgrade(int id)
        {
            return GetObject(id) as Upgrade;
        }

        public BaseObject GetObject(int id)
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

        public Tech GetTech(int id)
        {
            if (Enum.IsDefined(typeof(TechEquivalent), id))
            {
                return new Tech((TechEquivalent)id);
            }

            var baseObject = GetObject(id);
            if (baseObject != null && (baseObject is Unit || baseObject is Upgrade))
            {
                return new Tech(this, id);
            }

            return null;
        }

        public MapObjectData GetAllData()
        {
            return new MapObjectData()
            {
                UnitData = new MapUnitObjectData(_objects.Where(pair => pair.Value is Unit).Select(pair => pair.Value.ObjectModification).ToArray()),
                ItemData = new MapItemObjectData(_objects.Where(pair => pair.Value is Item).Select(pair => pair.Value.ObjectModification).ToArray()),
                DestructableData = new MapDestructableObjectData(_objects.Where(pair => pair.Value is Destructable).Select(pair => pair.Value.ObjectModification).ToArray()),
                DoodadData = new MapDoodadObjectData(_objects.Where(pair => pair.Value is Doodad).Select(pair => pair.Value.ObjectModification).ToArray()),
                AbilityData = new MapAbilityObjectData(_objects.Where(pair => pair.Value is Ability).Select(pair => pair.Value.ObjectModification).ToArray()),
                BuffData = new MapBuffObjectData(_objects.Where(pair => pair.Value is Buff).Select(pair => pair.Value.ObjectModification).ToArray()),
                UpgradeData = new MapUpgradeObjectData(_objects.Where(pair => pair.Value is Upgrade).Select(pair => pair.Value.ObjectModification).ToArray()),
            };
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

            if (_reservedKeys.Contains(baseObject.Key))
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