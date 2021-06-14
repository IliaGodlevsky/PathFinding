using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                collection.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            collection.AddRange(items.AsEnumerable());
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                collection.Remove(item);
            }
        }
    }
}
