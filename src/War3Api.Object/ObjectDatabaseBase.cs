using System.Diagnostics.CodeAnalysis;

namespace War3Api.Object
{
    public abstract class ObjectDatabaseBase
    {
        private readonly ObjectDatabaseBase? _fallbackDatabase;

        internal ObjectDatabaseBase(ObjectDatabaseBase? fallbackDatabase)
        {
            _fallbackDatabase = fallbackDatabase;
        }

        internal ObjectDatabaseBase? FallbackDatabase => _fallbackDatabase;

        public abstract bool TryGetObject(int id, [NotNullWhen(true)] out BaseObject? baseObject);

        internal abstract void AddObject(BaseObject baseObject);

        internal abstract void ReserveTech(int id);
    }
}