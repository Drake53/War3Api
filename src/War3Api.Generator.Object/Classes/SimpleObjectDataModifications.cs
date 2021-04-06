using System.Collections;
using System.Collections.Generic;

using War3Net.Build.Object;

namespace War3Api.Object
{
    public sealed class SimpleObjectDataModifications : IEnumerable<SimpleObjectDataModification>
    {
        private readonly Dictionary<int, SimpleObjectDataModification> _modifications = new();

        public SimpleObjectDataModification this[int key]
        {
            get => _modifications[key];
            set => _modifications[key] = value;
        }

        public IEnumerator<SimpleObjectDataModification> GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }
    }
}