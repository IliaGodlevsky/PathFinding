using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsCardinal(this int[] coordinates, int[] neighbourCoordinates)
        {
            if (coordinates.Length != neighbourCoordinates.Length
                || coordinates.Length == 0 || neighbourCoordinates.Length == 0)
            {
                return false;
            }

            bool IsNotEqual(int i) => coordinates[i] != neighbourCoordinates[i];
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            return Enumerable.Range(0, coordinates.Length).IsSingle(IsNotEqual);
        }

        public static IEnumerable<IVertex> FilterObstacles(this IEnumerable<IVertex> collection)
        {
            return collection.Where(vertex => !vertex.IsObstacle);
        }

        public static IEnumerable<IVertex> Without(this IEnumerable<IVertex> self, IEndPoints endPoints)
        {
            return self.Without(endPoints.EndPoints);
        }

        public static IVertex DequeueOrNullVertex(this Queue<IVertex> queue)
        {
            return queue.Count == 0 ? NullVertex.Instance : queue.Dequeue();
        }

        /// <summary>
        /// Returns the first vertex of the sequence using <paramref name="predicate"/> 
        /// of returns <see cref="NullVertex"/> if the sequence is empty or 
        /// no element passes the <paramref name="predicate"/>
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection, Func<IVertex, bool> predicate)
        {
            return collection.FirstOrDefault(predicate) ?? NullVertex.Instance;
        }

        public static Dictionary<ICoordinate, IVertex> ToDictionary(this IEnumerable<IVertex> vertices)
        {
            return vertices.ToDictionary(vertex => vertex.Position);
        }

        /// <summary>
        /// Converts <paramref name="index"/> 
        /// into an array of cartesian coordinates 
        /// according to graph dimension sizes
        /// </summary>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns>An array of cartesian coordinates</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when 
        /// index is greater of equals
        /// <paramref name="dimensionSizes"/> 
        /// elements multiplication </exception>
        public static int[] ToCoordinates(this int[] dimensionSizes, int index)
        {
            int size = dimensionSizes.GetMultiplication();
            var rangeOfIndices = new InclusiveValueRange<int>(size - 1, 0);
            if (!rangeOfIndices.Contains(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var coordinates = new int[dimensionSizes.Length];

            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = index % dimensionSizes[i];
                index /= dimensionSizes[i];
            }

            return coordinates;
        }
    }
}
