using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            if (items == null || property == null) 
            {
                throw new ArgumentException("Bad incoming arguments");
            }

            return items.GroupBy(property).Select(item => item.First());
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        public static int MaxOrDefault(this IEnumerable<int> collection)
        {
            return collection.Any() ? collection.Max() : default;
        }
    }
}
