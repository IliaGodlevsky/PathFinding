using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// At each step skips the number of elements equal 
        /// to the number of loop iterations and applies an 
        /// aggregate function to the remaining elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns>Result of aggregation</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of arguments is null</exception>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IEnumerable<T> StepAggregate<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            return collection.Select((arr, i) => arr.Skip(i).Aggregate(func));
        }

        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T,T,T> func)
        {
            return collection.Any() ? collection.Aggregate(func) : default;
        }

        internal static IEnumerable<T> ForEachIndex<T>(this IEnumerable<T> collection, Action<int> action)
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                action(i);
            }

            return collection;
        }

        private static IEnumerable<T> Select<T>(this IEnumerable<T> collection, Func<IEnumerable<T>, int, T> func)
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                yield return func(collection, i);
            }
        }
    }
}
