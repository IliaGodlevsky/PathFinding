using GraphLib.Comparers;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Abstractions;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class ICoordinateExtensions
    {
        internal static bool IsEqual(this ICoordinate self, ICoordinate coordinates)
        {
            if (self == null || coordinates == null)
            {
                return false;
            }

            return self.Coordinates.SequenceEqual(coordinates.Coordinates);
        }

        internal static bool IsWithinGraph(this ICoordinate coordinates, IGraph graph)
        {
            var dimensionComparer = new DimensionComparer();
            return coordinates.Coordinates.SequenceEqual(graph.DimensionsSizes, dimensionComparer);
        }
    }
}
