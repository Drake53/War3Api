using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using War3Net.Build.Object;

namespace War3Api.Object
{
    public sealed class VariationObjectDataModifications : IEnumerable<VariationObjectDataModification>
    {
        private readonly BaseObject _baseObject;
        private readonly Dictionary<long, VariationObjectDataModification> _modifications;

        internal VariationObjectDataModifications(BaseObject baseObject)
        {
            _baseObject = baseObject;
            _modifications = new();
        }

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

        internal VariationObjectDataModification GetModification(int key) => GetModification((long)key);

        internal VariationObjectDataModification GetModification(int key, int variation) => GetModification(GetKey(key, variation));

#pragma warning disable CS0675
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long GetKey(int key, int variation) => key | ((long)variation << 32);
#pragma warning restore CS0675

        private VariationObjectDataModification GetModification(long key)
        {
            if (_modifications.TryGetValue(key, out var modification))
            {
                return modification;
            }

            var fallback = _baseObject.Db.FallbackDatabase;
            while (fallback is not null)
            {
                if (fallback is LazyStandardObjectDatabase)
                {
                    if (fallback.TryGetObject(_baseObject.OldId, out var fallbackObject) && fallbackObject.TryGetVariationModifications(out var fallbackModifications))
                    {
                        return fallbackModifications.GetModification(key);
                    }
                }
                else
                {
                    if (fallback.TryGetObject(_baseObject.Key, out var fallbackObject) && _baseObject.OldId == fallbackObject.OldId && fallbackObject.TryGetVariationModifications(out var fallbackModifications))
                    {
                        return fallbackModifications.GetModification(key);
                    }
                }

                fallback = fallback.FallbackDatabase;
            }

            throw new KeyNotFoundException();
        }
    }
}