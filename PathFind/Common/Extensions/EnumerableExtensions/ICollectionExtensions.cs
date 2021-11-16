using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions.EnumerableExtensions
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            range.ForEach(collection.Add);
        }

        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            collection.AddRange(items.AsEnumerable());
        }
    }
}
