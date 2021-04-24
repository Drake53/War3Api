#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public IEnumerable<Unit> GetUnits()
        {
            return _objects.CastWhere<int, BaseObject, Unit>();
        }

        public IEnumerable<Item> GetItems()
        {
            return _objects.CastWhere<int, BaseObject, Item>();
        }

        public IEnumerable<Destructable> GetDestructables()
        {
            return _objects.CastWhere<int, BaseObject, Destructable>();
        }

        public IEnumerable<Doodad> GetDoodads()
        {
            return _objects.CastWhere<int, BaseObject, Doodad>();
        }

        public IEnumerable<Ability> GetAbilities()
        {
            return _objects.CastWhere<int, BaseObject, Ability>();
        }

        public IEnumerable<Buff> GetBuffs()
        {
            return _objects.CastWhere<int, BaseObject, Buff>();
        }

        public IEnumerable<Upgrade> GetUpgrades()
        {
            return _objects.CastWhere<int, BaseObject, Upgrade>();
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

        public ObjectData GetAllData(ObjectDataFormatVersion formatVersion = ObjectDataFormatVersion.Normal)
        {
            var units = GetUnits().Select(unit => (SimpleObjectModification)unit);
            var items = GetItems().Select(item => (SimpleObjectModification)item);
            var destructables = GetDestructables().Select(destructable => (SimpleObjectModification)destructable);
            var doodads = GetDoodads().Select(doodad => (VariationObjectModification)doodad);
            var abilities = GetAbilities().Select(ability => (LevelObjectModification)ability);
            var buffs = GetBuffs().Select(buff => (SimpleObjectModification)buff);
            var upgrades = GetUpgrades().Select(upgrade => (LevelObjectModification)upgrade);

            return new ObjectData(formatVersion)
            {
                UnitData = units.Any() ? new MapUnitObjectData(formatVersion)
                {
                    BaseUnits = units.Where(unit => unit.NewId == 0).ToList(),
                    NewUnits = units.Where(unit => unit.NewId != 0).ToList(),
                } : null,
                ItemData = items.Any() ? new MapItemObjectData(formatVersion)
                {
                    BaseItems = items.Where(item => item.NewId == 0).ToList(),
                    NewItems = items.Where(item => item.NewId != 0).ToList(),
                } : null,
                DestructableData = destructables.Any() ? new MapDestructableObjectData(formatVersion)
                {
                    BaseDestructables = destructables.Where(destructable => destructable.NewId == 0).ToList(),
                    NewDestructables = destructables.Where(destructable => destructable.NewId != 0).ToList(),
                } : null,
                DoodadData = doodads.Any() ? new MapDoodadObjectData(formatVersion)
                {
                    BaseDoodads = doodads.Where(doodad => doodad.NewId == 0).ToList(),
                    NewDoodads = doodads.Where(doodad => doodad.NewId != 0).ToList(),
                } : null,
                AbilityData = abilities.Any() ? new MapAbilityObjectData(formatVersion)
                {
                    BaseAbilities = abilities.Where(ability => ability.NewId == 0).ToList(),
                    NewAbilities = abilities.Where(ability => ability.NewId != 0).ToList(),
                } : null,
                BuffData = buffs.Any() ? new MapBuffObjectData(formatVersion)
                {
                    BaseBuffs = buffs.Where(buff => buff.NewId == 0).ToList(),
                    NewBuffs = buffs.Where(buff => buff.NewId != 0).ToList(),
                } : null,
                UpgradeData = upgrades.Any() ? new MapUpgradeObjectData(formatVersion)
                {
                    BaseUpgrades = upgrades.Where(upgrade => upgrade.NewId == 0).ToList(),
                    NewUpgrades = upgrades.Where(upgrade => upgrade.NewId != 0).ToList(),
                } : null,
            };
        }

        internal void AddObject(BaseObject baseObject)
        {
            if (_objects.ContainsKey(baseObject.Key))
            {
                throw new ArgumentException($"An object with key '{baseObject.Key.ToRawcode()}' has already been added to this database.");
            }

            if (baseObject is Unit && !Enum.IsDefined(typeof(UnitType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(UnitType));
            }

            if (baseObject is Item && !Enum.IsDefined(typeof(ItemType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(ItemType));
            }

            if (baseObject is Destructable && !Enum.IsDefined(typeof(DestructableType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(DestructableType));
            }

            if (baseObject is Doodad && !Enum.IsDefined(typeof(DoodadType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(DoodadType));
            }

            if (baseObject is Ability && !Enum.IsDefined(typeof(AbilityType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(AbilityType));
            }

            if (baseObject is Buff && !Enum.IsDefined(typeof(BuffType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(BuffType));
            }

            if (baseObject is Upgrade && !Enum.IsDefined(typeof(UpgradeType), baseObject.OldId))
            {
                throw new InvalidEnumArgumentException(nameof(baseObject.OldId), baseObject.OldId, typeof(UpgradeType));
            }

            if (baseObject.NewId != 0)
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