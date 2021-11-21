using System.Diagnostics.CodeAnalysis;

using War3Api.Object.Enums;

using War3Net.Common.Extensions;

namespace War3Api.Object
{
    // todo: generate through code (since it's similar to Unit/Ability/etc)
    public abstract class BaseObject
    {
        private readonly ObjectDatabaseBase _db;

        internal BaseObject(int oldId, ObjectDatabaseBase? db = null)
        {
            OldId = oldId;
            NewId = 0;

            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        internal BaseObject(int oldId, int newId, ObjectDatabaseBase? db = null)
        {
            OldId = oldId;
            NewId = newId;

            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        internal BaseObject(int oldId, string newRawcode, ObjectDatabaseBase? db = null)
        {
            OldId = oldId;
            NewId = newRawcode.FromRawcode();

            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        public int OldId { get; }

        public int NewId { get; }

        public ObjectDatabaseBase Db => _db;

        public int Key => NewId != 0 ? NewId : OldId;

        internal static BaseObject ShallowCopy(BaseObject baseObject, ObjectDatabaseBase db)
        {
            return baseObject switch
            {
                Unit => new Unit((UnitType)baseObject.OldId, baseObject.NewId, db),
                Item => new Item((ItemType)baseObject.OldId, baseObject.NewId, db),
                Destructable => new Destructable((DestructableType)baseObject.OldId, baseObject.NewId, db),
                Doodad => new Doodad((DoodadType)baseObject.OldId, baseObject.NewId, db),
                Ability => AbilityFactory.Create((AbilityType)baseObject.OldId, baseObject.NewId, db),
                Buff => new Buff((BuffType)baseObject.OldId, baseObject.NewId, db),
                Upgrade => new Upgrade((UpgradeType)baseObject.OldId, baseObject.NewId, db),
            };
        }

        internal virtual bool TryGetLevelModifications([NotNullWhen(true)] out LevelObjectDataModifications? modifications)
        {
            modifications = null;
            return false;
        }

        internal virtual bool TryGetSimpleModifications([NotNullWhen(true)] out SimpleObjectDataModifications? modifications)
        {
            modifications = null;
            return false;
        }

        internal virtual bool TryGetVariationModifications([NotNullWhen(true)] out VariationObjectDataModifications? modifications)
        {
            modifications = null;
            return false;
        }
    }
}