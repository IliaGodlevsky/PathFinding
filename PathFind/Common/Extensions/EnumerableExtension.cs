using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumerableExtension
    {
        private static readonly Random rand;

        static EnumerableExtension()
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

            T FirstOfTheGroup(IGrouping<TKey, T> item) => item.First();

            return collection
                .GroupBy(selector)
                .Select(FirstOfTheGroup);
        }

        /// <summary>
        /// Returns random element of <paramref name="self"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="self"></param>
        /// <returns>Random element of <paramref name="self"/></returns>
        public static TSource RandomElementOrDefault<TSource>(this IEnumerable<TSource> self)
        {
            var collection = self.ToArray();
            var index = rand.Next(collection.Length);
            return collection.ElementAtOrDefault(index);
        }

        public static IEnumerable<TSource> Which<TSource>(this IEnumerable<TSource> self,
            Func<TSource, bool> predicate)
        {
            return self.Where(predicate);
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

        public static double SumOrDefault(this IEnumerable<int> collection)
        {
            return collection.Any() ? collection.Sum() : default;
        }

        public static double SumOrDefault(this IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Sum() : default;
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
