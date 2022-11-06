using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class ICollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            range.ForEach(collection.Add);
            return collection;
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            return collection.AddRange(items.AsEnumerable());
        }
    }
}
