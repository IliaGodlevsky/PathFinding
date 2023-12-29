using LiteDB;
using Pathfinding.App.Console.DataAccess;
using Shared.Extensions;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ILiteDbExtensions
    {
        public static ILiteCollection<T> GetNamedCollection<T>(this ILiteDatabase database)
        {
            string collectionName = typeof(T).GetAttributeOrDefault<BsonTableAttribute>().Name;
            return database.GetCollection<T>(collectionName, BsonAutoId.Int32);
        }
    }
}
