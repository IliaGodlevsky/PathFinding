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
            #region InvariantsObservance
            if (collection == null || selector == null)
            {
                throw new ArgumentNullException("Bad incoming arguments");
            }
            #endregion

            return collection
                .GroupBy(selector)
                .Select(item => item.First());
        }

        /// <summary>
        /// Returns random element of <paramref name="self"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="self"></param>
        /// <returns>Random element of <paramref name="self"/></returns>
        public static TSource GetRandomElement<TSource>(this IEnumerable<TSource> self)
        {
            var index = rand.Next(self.Count());
            return self.ElementAt(index);
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
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, 
            Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        /// <summary>
        /// Applies delegate <paramref name="action"/> 
        /// to each element of <paramref name="collection"/>
        /// adding array index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, 
            Action<T, int> action)
        {
            Enumerable
                .Range(0, self.Count())
                .ForEach(i => action(self.ElementAt(i), i));

            return self;
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

        /// <summary>
        /// Applies <paramref name="predicate"/> to corresponding 
        /// elements of sequencies and returns true if <paramref name="predicate"/> 
        /// is true for all pairs of elements and false if not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="second"></param>
        /// <param name="predicate"></param>
        /// <returns>true if <paramref name="predicate"/> is true 
        /// for each corresponding elements in two sequencies and false if not</returns>
        public static bool Match<T>(this IEnumerable<T> self, 
            IEnumerable<T> second, Func<T, T, bool> predicate)
        {
            #region InvariantsObservance
            if (self.Count() != second.Count())
            {
                return false;
            }

            if (!self.Any() && !second.Any())
            {
                return true;
            }
            #endregion

            return Enumerable
                .Range(0, second.Count())
                .All(i => predicate(self.ElementAt(i), second.ElementAt(i)));
        }
    }
}
