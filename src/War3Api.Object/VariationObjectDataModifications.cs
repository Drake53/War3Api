using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
            get => _modifications[GetKey(key, variation)];
            set => _modifications[GetKey(key, variation)] = value;
        }

        public bool ContainsKey(int key) => _modifications.ContainsKey(key);

        public bool ContainsKey(int key, int variation) => _modifications.ContainsKey(GetKey(key, variation));

        public bool TryGetValue(int key, [NotNullWhen(true)] out VariationObjectDataModification? modification) => _modifications.TryGetValue(key, out modification);

        public bool TryGetValue(int key, int variation, [NotNullWhen(true)] out VariationObjectDataModification? modification) => _modifications.TryGetValue(GetKey(key, variation), out modification);

        public IEnumerator<VariationObjectDataModification> GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

#pragma warning disable CS0675
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long GetKey(int key, int variation) => key | ((long)variation << 32);
#pragma warning restore CS0675
    }
}