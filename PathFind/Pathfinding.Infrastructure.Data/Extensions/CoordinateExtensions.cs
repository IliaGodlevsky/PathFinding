using Pathfinding.Shared.Primitives;
using System;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Extensions
{
    public static class CoordinateExtensions
    {
        public static int GetX(this Coordinate coordinate)
        {
            return coordinate.CoordinatesValues.ElementAtOrDefault(0);
        }

        public static int GetY(this Coordinate coordinate)
        {
            return coordinate.CoordinatesValues.ElementAtOrDefault(1);
        }

        public static bool IsCardinal(this Coordinate self, Coordinate coordinate)
        {
            // Cardinal coordinate differs from the
            // central one only for single coordinate value
            var difference = self.CoordinatesValues
                .Zip(coordinate.CoordinatesValues, (x, y) => Math.Abs(x - y))
                .Sum();
            return difference == 1;
        }
    }
}
