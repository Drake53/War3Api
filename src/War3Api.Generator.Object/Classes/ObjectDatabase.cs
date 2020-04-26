using System;
using System.Collections.Generic;
using System.Linq;

using War3Net.Build.Object;

namespace War3Api.Object
{
    public sealed class ObjectDatabase
    {
        private static Lazy<ObjectDatabase> _defaultDatabase => new Lazy<ObjectDatabase>(() => new ObjectDatabase());

        private readonly HashSet<int> _reservedKeys;
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
            _objects = objects.ToDictionary(obj => obj.Key);
        }

        public static ObjectDatabase DefaultDatabase => _defaultDatabase.Value;

        /*public Unit this[int id]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }*/

        // todo: possibly create new if not found? (only possible if id matches a base object type)
        public Unit GetUnit(int id)
        {
            return _objects.Where(pair => pair.Value is Unit unit && unit.Key == id).Select(pair => pair.Value as Unit).SingleOrDefault();
        }

        public Item GetItem(int id)
        {
            return _objects.Where(pair => pair.Value is Item item && item.Key == id).Select(pair => pair.Value as Item).SingleOrDefault();
        }

        public Destructable GetDestructable(int id)
        {
            return _objects.Where(pair => pair.Value is Destructable destructable && destructable.Key == id).Select(pair => pair.Value as Destructable).SingleOrDefault();
        }

        public Doodad GetDoodad(int id)
        {
            return _objects.Where(pair => pair.Value is Doodad doodad && doodad.Key == id).Select(pair => pair.Value as Doodad).SingleOrDefault();
        }

        public Ability GetAbility(int id)
        {
            return _objects.Where(pair => pair.Value is Ability ability && ability.Key == id).Select(pair => pair.Value as Ability).SingleOrDefault();
        }

        public Buff GetBuff(int id)
        {
            return _objects.Where(pair => pair.Value is Buff buff && buff.Key == id).Select(pair => pair.Value as Buff).SingleOrDefault();
        }

        public Upgrade GetUpgrade(int id)
        {
            return _objects.Where(pair => pair.Value is Upgrade upgrade && upgrade.Key == id).Select(pair => pair.Value as Upgrade).SingleOrDefault();
        }

        public Tech GetTech(int id)
        {
            var tech = new Tech();
            tech.Key = id;

            if (tech.AsUnit != null || tech.AsUpgrade != null || Enum.IsDefined(typeof(TechEquivalent), id))
            {
                return tech;
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

        private static IEnumerable<int> GetDefaultReservedKeys()
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

            foreach (var techEquivalent in Enum.GetValues(typeof(TechEquivalent)))
            {
                yield return (int)techEquivalent;
            }

            // todo: set default reserved keys (random unit/item, etc)
        }
    }
}