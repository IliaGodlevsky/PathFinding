using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class EnumerableExtension
    {
        private static readonly Random Random;

        static EnumerableExtension()
        {
            Random = new Random();
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
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> selector)
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

        public static bool Remove<T>(this Queue<T> queue, T item)
        {
            bool isRemoved = false;
            if (queue.Contains(item))
            {
                var items = new List<T>(queue);
                queue.Clear();
                isRemoved = items.Remove(item);
                items.ForEach(queue.Enqueue);
            }
            return isRemoved;
        }

        /// <summary>
        /// Applies an accumulation function over the collection 
        /// or returns default value if collection is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns>A result of aggregation of the collection</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            var items = collection.ToArray();
            return items.Any() ? items.Aggregate(func) : default;
        }

        public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Any() ? source.Average(selector) : default;
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
            var index = Random.Next(collection.Length);
            return collection.ElementAtOrDefault(index);
        }

        /// <summary>
        /// Determins, whether the sequence 
        /// contains all of the <paramref name="items"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool ContainsAll<T>(this IEnumerable<T> self, params T[] items)
        {
            bool IsInCollection(T item)
            {
                return self.Any(selfItems => ReferenceEquals(selfItems, item));
            }

            return items.All(IsInCollection);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> self, params T[] items)
        {
            return self.Except(items.AsEnumerable());
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
            foreach (var item in collection.ToArray())
            {
                action(item);
            }

            return collection;
        }

        public static bool IsSingle<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Count(predicate) == 1;
        }

        public static double SumOrDefault(this IEnumerable<double> collection)
        {
            return collection.Any() ? collection.Sum() : default;
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
            var firstArray = self.ToArray();
            var secondArray = second.ToArray();
            #region InvariantsObservance
            if (!self.HaveEqualLength(second))
            {
                return false;
            }

            if (!firstArray.Any() || !secondArray.Any())
            {
                return false;
            }
            #endregion
            bool Matches(int i) => predicate(firstArray[i], secondArray[i]);
            return Enumerable.Range(0, secondArray.Length).All(Matches);
        }

        public static bool HaveEqualLength<T>(this IEnumerable<T> self, IEnumerable<T> collection)
        {
            return self.Count() == collection.Count();
        }

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>
            (this IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            return collection.ToDictionary(item => item.Key, item => item.Value);
        }

        public static IDictionary<string, T> ToNameInstanceDictionary<T>(this IEnumerable<T> collection)
        {
            return collection.ToDictionary(item => item.GetDescriptionAttributeValueOrTypeName());
        }

        public static bool TryGetKey<TKey, TValue>(this IDictionary<TKey, TValue> self, TValue value, out TKey key)
        {
            if (self.Values.Contains(value))
            {
                key = self.FirstOrDefault(item => item.Value.Equals(value)).Key;
                return true;
            }

            key = default;
            return false;
        }
    }
}
