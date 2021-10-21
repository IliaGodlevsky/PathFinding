using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumerableExtension
    {
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
                var items = new LinkedList<T>(queue);
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

        /// <summary>
        /// Determins, whether the sequence 
        /// contains all of the <paramref name="items"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        /// <remarks>This method compares elements by references</remarks>
        public static bool ContainsReferences<T>(this IEnumerable<T> self, params T[] items)
            where T : class
        {
            bool IsInCollection(T item)
            {
                return self.Any(selfItems => ReferenceEquals(selfItems, item));
            }

            return items.All(IsInCollection);
        }

        public static IEnumerable<T> Without<T>(this IEnumerable<T> self, params T[] items)
        {
            return self.Where(item => !items.Contains(item));
        }

        public static IEnumerable<T> Without<T>(this IEnumerable<T> self, IEnumerable<T> items)
        {
            return self.Where(item => !items.Contains(item));
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

        public static double MinOrDefault<T>(this IEnumerable<T> collection, Func<T, double> selector)
        {
            return collection.Any() ? collection.Min(selector) : default;
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
            const int ZeroLength = default;
            var firstArray = self.ToArray();
            var secondArray = second.ToArray();
            #region InvariantsObservance
            if (firstArray.Length != secondArray.Length
                || ZeroLength.IsOneOf(firstArray.Length, secondArray.Length))
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

        public static Tuple<string, T>[] ToOrderedNameInstanceTuples<T, U>(this IEnumerable<T> collection, Func<Tuple<string, T>, U> orderSelector)
        {
            return collection
                .Select(item => new Tuple<string, T>(item.GetDescriptionAttributeValueOrTypeName(), item))
                .OrderBy(orderSelector)
                .ToArray();
        }
    }
}
