using War3Net.Common.Extensions;

namespace War3Api.Object
{
    // todo: generate through code (since it's similar to Unit/Ability/etc)
    public abstract class BaseObject
    {
        private readonly ObjectDatabase _db;

        internal BaseObject(int oldId, ObjectDatabase db = null)
        {
            OldId = oldId;
            NewId = 0;

            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        internal BaseObject(int oldId, int newId, ObjectDatabase db = null)
        {
            OldId = oldId;
            NewId = newId;

            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        internal BaseObject(int oldId, string newRawcode, ObjectDatabase db = null)
        {
            OldId = oldId;
            NewId = newRawcode.FromRawcode();

            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        public int OldId { get; set; }

        public int NewId { get; set; }

        public ObjectDatabase Db => _db;

        public int Key => NewId != 0 ? NewId : OldId;
    }
}