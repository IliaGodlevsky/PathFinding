using LiteDB;
using Shared.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ILiteDbExtensions
    {
        public static ILiteCollection<T> GetNamedCollection<T>(this ILiteDatabase database)
        {
            string collectionName = typeof(T).GetAttributeOrDefault<TableAttribute>().Name;
            return database.GetCollection<T>(collectionName, BsonAutoId.Int32);
        }
    }
}
