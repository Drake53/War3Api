using War3Net.Common.Extensions;

namespace War3Api.Object
{
    // todo: generate through code (since it's similar to Unit/Ability/etc)
    public abstract class BaseObject
    {
        private ObjectDatabase _db;

        internal BaseObject(int oldId)
        {
            OldId = oldId;
            NewId = 0;
        }

        internal BaseObject(int oldId, int newId)
        {
            OldId = oldId;
            NewId = newId;
        }

        internal BaseObject(int oldId, string newRawcode)
        {
            OldId = oldId;
            NewId = newRawcode.FromRawcode();
        }

        public int OldId { get; }

        public int NewId { get; }

        public ObjectDatabase Db
        {
            get => _db;
            internal set => _db = value;
        }

        public int Key => NewId != 0 ? NewId : OldId;
    }
}