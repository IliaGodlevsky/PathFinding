using Pathfinding.Domain.Interface;
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

        public static int GetZ(this Coordinate coordinate)
        {
            return coordinate.CoordinatesValues.ElementAtOrDefault(2);
        }

        public static bool IsCardinal(this Coordinate self, Coordinate coordinate)
        {
            // Cardinal coordinate differs from the
            // central one only for single coordinate value
            int limit = Math.Min(self.Count, coordinate.Count);
            int differentCount = 0;
            while (limit-- > 0 && differentCount <= 1)
            {
                if (self[limit] != coordinate[limit])
                {
                    differentCount++;
                }
            }
            return differentCount == 1;
        }
    }
}
