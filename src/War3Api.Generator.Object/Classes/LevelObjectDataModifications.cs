using System.Collections;
using System.Collections.Generic;

using War3Net.Build.Object;

namespace War3Api.Object
{
    public sealed class LevelObjectDataModifications : IEnumerable<LevelObjectDataModification>
    {
        private readonly Dictionary<long, LevelObjectDataModification> _modifications = new();

        public LevelObjectDataModification this[int key]
        {
            get => _modifications[key];
            set => _modifications[key] = value;
        }

        public LevelObjectDataModification this[int key, int level]
        {
            get => _modifications[key | (level << 32)];
            set => _modifications[key | (level << 32)] = value;
        }

        public IEnumerator<LevelObjectDataModification> GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }
    }
}