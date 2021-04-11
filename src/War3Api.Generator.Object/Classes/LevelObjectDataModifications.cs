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
#pragma warning disable CS0675
            get => _modifications[key | ((long)level << 32)];
            set => _modifications[key | ((long)level << 32)] = value;
#pragma warning restore CS0675
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