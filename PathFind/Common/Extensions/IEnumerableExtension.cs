using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class IEnumerableExtension
    {
        private static readonly Random rand;

        static IEnumerableExtension()
        {
            rand = new Random();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            if (items == null || property == null) 
            {
                throw new ArgumentException("Bad incoming arguments");
            }

            return items.GroupBy(property)
                .Select(item => item.First());
        }

        public static TSource GetRandomElement<TSource>(this IEnumerable<TSource> collection)
        {
            var randomIndex = rand.Next(collection.Count());
            return collection.ElementAt(randomIndex);
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

        public static double MaxOrDefault(this IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Max() : default;
        }
    }
}
