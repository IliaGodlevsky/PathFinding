using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsCardinal(this int[] coordinates, int[] neighbourCoordinates)
        {
            if (!coordinates.HaveEqualLength(neighbourCoordinates)
                || coordinates.Length == 0 || neighbourCoordinates.Length == 0
                || !coordinates.IsClose(neighbourCoordinates))
            {
                return false;
            }

            bool IsNotEqual(int i) => coordinates[i] != neighbourCoordinates[i];
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            return Enumerable.Range(0, coordinates.Length).IsSingle(IsNotEqual);
        }

        public static bool IsClose(this int[] coordinates, int[] neighbourCoordinates)
        {
            return coordinates.Match(neighbourCoordinates, (a, b) => Math.Abs(a - b) <= 1);
        }

        public static T AggregateOrDefault<T>(this IEnumerable<T> collection, Func<T, T, T> func)
        {
            var items = collection.ToArray();
            return items.Any() ? items.Aggregate(func) : default;
        }

        public static IVertex[] FilterObstacles(this IEnumerable<IVertex> collection)
        {
            return collection.Where(vertex => !vertex.IsObstacle).ToArray();
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
    }
}
