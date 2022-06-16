using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;

namespace GraphLib.Extensions
{
    public static class CoordinateExtensions
    {
        public static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            if (self == null || coordinate == null)
            {
                return false;
            }

            return self.CoordinatesValues.Juxtapose(coordinate.CoordinatesValues);
        }

        public static bool IsCardinal(this ICoordinate coordinate, ICoordinate neighbour)
        {
            return coordinate.CoordinatesValues.IsCardinal(neighbour.CoordinatesValues);
        }
    }
}
