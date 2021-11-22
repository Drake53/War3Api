using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using War3Api.Object.Enums;

using War3Net.Common.Extensions;

namespace War3Api.Object
{
    internal sealed class LazyStandardObjectDatabase : ObjectDatabaseBase
    {
        private static readonly LazyStandardObjectDatabase _standardDatabase = new();

        private readonly UnitLoader _unitLoader;
        private readonly ItemLoader _itemLoader;
        private readonly DestructableLoader _destructableLoader;
        private readonly DoodadLoader _doodadLoader;
        private readonly AbilityLoader _abilityLoader;
        private readonly BuffLoader _buffLoader;
        private readonly UpgradeLoader _upgradeLoader;
        private readonly Dictionary<int, BaseObject> _objects;

        private LazyStandardObjectDatabase()
            : base(null)
        {
            _unitLoader = new();
            _itemLoader = new();
            _destructableLoader = new();
            _doodadLoader = new();
            _abilityLoader = new();
            _buffLoader = new();
            _upgradeLoader = new();
            _objects = new();
        }

        public override bool TryGetObject(int id, [NotNullWhen(true)] out BaseObject? baseObject)
        {
            if (_objects.TryGetValue(id, out baseObject))
            {
                return true;
            }

            if (TryLoadObject(id, out baseObject))
            {
                return true;
            }

            var fallback = FallbackDatabase;
            while (fallback is not null)
            {
                if (fallback.TryGetObject(id, out var fallbackObject))
                {
                    baseObject = BaseObject.ShallowCopy(fallbackObject, this);
                    return true;
                }

                fallback = fallback.FallbackDatabase;
            }

            return false;
        }

        internal static LazyStandardObjectDatabase GetStandardDatabase() => _standardDatabase;

        internal override void AddObject(BaseObject baseObject)
        {
            _objects.Add(baseObject.Key, baseObject);
        }

        internal override void ReserveTech(int id)
        {
        }

        private BaseObject LoadObject(int id)
        {
            if (Enum.IsDefined((UnitType)id))
            {
                return _unitLoader.Load((UnitType)id, this);
            }

            if (Enum.IsDefined((ItemType)id))
            {
                return _itemLoader.Load((ItemType)id, this);
            }

            if (Enum.IsDefined((DestructableType)id))
            {
                return _destructableLoader.Load((DestructableType)id, this);
            }

            if (Enum.IsDefined((DoodadType)id))
            {
                return _doodadLoader.Load((DoodadType)id, this);
            }

            if (Enum.IsDefined((AbilityType)id))
            {
                return _abilityLoader.Load((AbilityType)id, this);
            }

            if (Enum.IsDefined((BuffType)id))
            {
                return _buffLoader.Load((BuffType)id, this);
            }

            if (Enum.IsDefined((UpgradeType)id))
            {
                return _upgradeLoader.Load((UpgradeType)id, this);
            }

            throw new ArgumentException($"Unknown base object type '{id.ToRawcode()}'.", nameof(id));
        }

        private bool TryLoadObject(int id, [NotNullWhen(true)] out BaseObject? baseObject)
        {
            if (!_objectTypes.Value.Contains(id))
            {
                baseObject = null;
                return false;
            }

            baseObject = LoadObject(id);
            return true;
        }
    }
}