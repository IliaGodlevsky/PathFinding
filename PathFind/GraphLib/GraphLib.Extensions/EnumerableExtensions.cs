using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsCardinal(this int[] coordinates, int[] neighbourCoordinates)
        {
            if (coordinates.Length != neighbourCoordinates.Length)
            {
                return false;
            }

            bool IsNotEqual(int i) => !coordinates[i].Equals(neighbourCoordinates[i]);
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            return Enumerable.Range(0, coordinates.Length).IsSingle(IsNotEqual);
        }

        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            var items = collection.ToArray();
            return items.Any() ? items.Aggregate(func) : default;
        }

        public static bool IsSingle<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Count(predicate) == 1;
        }
    }
}
