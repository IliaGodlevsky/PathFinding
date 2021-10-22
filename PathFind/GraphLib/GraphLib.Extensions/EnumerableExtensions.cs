using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EnumerableExtensions
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

        public static int ToHashCode(this IEnumerable<int> array)
        {
            return array.AggregateOrDefault(IntExtensions.Xor);
        }
    }
}
