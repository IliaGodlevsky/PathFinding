using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<IVertex> Without(this IEnumerable<IVertex> self, IIntermediateEndPoints endPoints)
        {
            return self.Without(endPoints.GetVertices());
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
    }
}
