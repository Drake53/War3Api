using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using War3Net.Build.Object;

namespace War3Api.Object
{
    public sealed class SimpleObjectDataModifications : IEnumerable<SimpleObjectDataModification>
    {
        private readonly BaseObject _baseObject;
        private readonly Dictionary<int, SimpleObjectDataModification> _modifications;

        internal SimpleObjectDataModifications(BaseObject baseObject)
        {
            _baseObject = baseObject;
            _modifications = new();
        }

        public SimpleObjectDataModification this[int key]
        {
            get => _modifications[key];
            set => _modifications[key] = value;
        }

        public bool ContainsKey(int key) => _modifications.ContainsKey(key);

        public bool TryGetValue(int key, [NotNullWhen(true)] out SimpleObjectDataModification? modification) => _modifications.TryGetValue(key, out modification);

        public IEnumerator<SimpleObjectDataModification> GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modifications.Values.GetEnumerator();
        }

        internal SimpleObjectDataModification GetModification(int key)
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
                    if (fallback.TryGetObject(_baseObject.OldId, out var fallbackObject) && fallbackObject.TryGetSimpleModifications(out var fallbackModifications))
                    {
                        return fallbackModifications.GetModification(key);
                    }
                }
                else
                {
                    if (fallback.TryGetObject(_baseObject.Key, out var fallbackObject) && _baseObject.OldId == fallbackObject.OldId && fallbackObject.TryGetSimpleModifications(out var fallbackModifications))
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