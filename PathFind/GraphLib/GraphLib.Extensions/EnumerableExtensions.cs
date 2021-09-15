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
            if (!coordinates.HaveEqualLength(neighbourCoordinates)
                || coordinates.Length == 0 || neighbourCoordinates.Length == 0)
            {
                return false;
            }

            bool IsNotEqual(int i) => coordinates[i] != neighbourCoordinates[i];
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            return Enumerable.Range(0, coordinates.Length).IsSingle(IsNotEqual);
        }

        public static IVertex[] FilterObstacles(this IEnumerable<IVertex> collection)
        {
            return collection.Where(vertex => !vertex.IsObstacle).ToArray();
        }
    }
}
