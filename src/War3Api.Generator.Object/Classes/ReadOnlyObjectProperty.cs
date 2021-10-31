using System;

namespace War3Api.Object
{
    public sealed class ReadOnlyObjectProperty<T>
    {
        private readonly Func<int, T> _getter;

        internal ReadOnlyObjectProperty(Func<int, T> getter)
        {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public T this[int level] => _getter(level);
    }
}