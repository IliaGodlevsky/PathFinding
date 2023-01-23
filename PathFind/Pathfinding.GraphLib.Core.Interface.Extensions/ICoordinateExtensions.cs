using System;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class ICoordinateExtensions
    {
        public static bool IsCardinal(this ICoordinate self, ICoordinate coordinate)
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
