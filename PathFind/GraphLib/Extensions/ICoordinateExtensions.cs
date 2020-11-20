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
            var dimensionSizes = graph.DimensionsSizes.ToArray();
            var currentCoordinates = coordinates.Coordinates.ToArray();

            return currentCoordinates.SequenceEqual(dimensionSizes, new DimensionComparer());
        }
    }
}
