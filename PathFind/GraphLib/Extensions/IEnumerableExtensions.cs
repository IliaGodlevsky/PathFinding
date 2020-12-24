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
        internal static IEnumerable<T> StepAggregate<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                yield return collection.Skip(i).Aggregate(func);
            }
        }
    }
}
