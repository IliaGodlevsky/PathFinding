using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class ICollectionExtensions
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            range.ForEach(collection.Add);
            return collection;
        }
    }
}
