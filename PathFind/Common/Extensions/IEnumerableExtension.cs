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

        /// <summary>
        /// Distincts <paramref name="collection"/> by <paramref name="selector"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <returns>A distincted <paramref name="collection"/></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> 
        /// is null or <paramref name="selector"/> is null</exception>
        public static IEnumerable<T> DistinctBy<T, TKey>(
            this IEnumerable<T> collection, Func<T, TKey> selector)
        {
            if (collection == null || selector == null)
            {
                throw new ArgumentNullException("Bad incoming arguments");
            }

            return collection.GroupBy(selector)
                .Select(item => item.First());
        }

        /// <summary>
        /// Returns random element of <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="collection"></param>
        /// <returns>Random element of <paramref name="collection"/></returns>
        public static TSource GetRandomElement<TSource>(this IEnumerable<TSource> collection)
        {
            var randomIndex = rand.Next(collection.Count());
            return collection.ElementAt(randomIndex);
        }

        /// <summary>
        /// Applies delegate <paramref name="action"/> 
        /// to each element of <paramref name="collection"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <returns>The same <paramref name="collection"/> with elements 
        /// to which <paramref name="action"/> was applied</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        /// <summary>
        /// Maximum int value of <paramref name="collection"/> 
        /// or 0 if <paramref name="collection"/> is empty
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Maximum int value or 0 if 
        /// <paramref name="collection"/> is empty</returns>
        public static int MaxOrDefault(this IEnumerable<int> collection)
        {
            return collection.Any() ? collection.Max() : default;
        }

        /// <summary>
        /// Maximum double value of <paramref name="collection"/> 
        /// or 0 if <paramref name="collection"/> is empty
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Maximum double value or 0 if 
        /// <paramref name="collection"/> is empty</returns>
        public static double MaxOrDefault(this IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Max() : default;
        }

        public static bool Match<T>(this IEnumerable<T> collection, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            if (collection.Count() != second.Count())
            {
                return false;
            }

            if (!collection.Any() && !second.Any())
            {
                return true;
            }

            int limit = second.Count();

            for (int i = 0; i < limit; i++)
            {
                T a = collection.ElementAt(i);
                T b = second.ElementAt(i);
                if (!comparer(a, b))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
