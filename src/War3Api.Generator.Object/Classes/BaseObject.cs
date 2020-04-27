using System;
using System.Collections.Generic;
using System.Text;

using War3Net.Build.Object;

namespace War3Api.Object
{
    // todo: generate through code (since it's similar to Unit/Ability/etc)
    public abstract class BaseObject
    {
        protected readonly ObjectModification _objectModification;

        private readonly ObjectDatabase _db;

        internal BaseObject(int oldId, ObjectDatabase db = null)
        {
            _objectModification = new ObjectModification(oldId, 0);
            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        internal BaseObject(int oldId, int newId, ObjectDatabase db = null)
        {
            _objectModification = new ObjectModification(oldId, newId);
            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        internal BaseObject(int oldId, string newRawcode, ObjectDatabase db = null)
        {
            _objectModification = new ObjectModification(oldId, newRawcode);
            _db = db ?? ObjectDatabase.DefaultDatabase;
            _db.AddObject(this);
        }

        public ObjectModification ObjectModification => _objectModification;

        public ObjectDatabase Db => _db;

        public int Key => _objectModification.NewId == 0 ? _objectModification.OldId : _objectModification.NewId;
    }
}