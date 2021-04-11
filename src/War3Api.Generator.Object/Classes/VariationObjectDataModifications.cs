using System.Collections;
using System.Collections.Generic;

using War3Net.Build.Object;

namespace War3Api.Object
{
    public sealed class VariationObjectDataModifications : IEnumerable<VariationObjectDataModification>
    {
        private readonly Dictionary<long, VariationObjectDataModification> _modifications = new();

        public VariationObjectDataModification this[int key]
        {
            get => _modifications[key];
            set => _modifications[key] = value;
        }

        public VariationObjectDataModification this[int key, int variation]
        {
#pragma warning disable CS0675
            get => _modifications[key | ((long)variation << 32)];
            set => _modifications[key | ((long)variation << 32)] = value;
#pragma warning restore CS0675
        }

        public IEnumerator<VariationObjectDataModification> GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }
    }
}