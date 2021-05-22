using GraphLib.Exceptions;
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
                throw new WrongNumberOfDimensionsException();
            }

            bool IsSubNotZero(int i) => coordinates[i] - neighbourCoordinates[i] != 0;
            return Enumerable.Range(0, coordinates.Length).Count(IsSubNotZero) == 1;
        }

        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            var items = collection.ToArray();
            return items.Any() ? items.Aggregate(func) : default;
        }
    }
}
