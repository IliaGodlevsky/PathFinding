using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            var items = collection.ToArray();
            return items.Any() ? items.Aggregate(func) : default;
        }
    }
}
