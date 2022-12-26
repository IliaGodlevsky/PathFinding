using System;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class ICoordinateExtensions
    {
        public static bool IsCardinal(this ICoordinate self, ICoordinate coordinate)
        {
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            int limit = Math.Min(self.Count, coordinate.Count);
            int count = 0;
            for (int i = 0; i < limit && count <= 1; i++)
            {
                if (self[i] != coordinate[i])
                {
                    count++;
                }
            }
            return count == 1;
        }
    }
}
