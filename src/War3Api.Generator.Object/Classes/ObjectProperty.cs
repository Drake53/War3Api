using System;

namespace War3Api.Object
{
    public sealed class ObjectProperty<T>
    {
        private readonly Func<int, T> _getter;
        private readonly Action<int, T> _setter;

        internal ObjectProperty(Func<int, T> getter, Action<int, T> setter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public T this[int level]
        {
            get => _getter(level);
            set => _setter(level, value);
        }
    }
}