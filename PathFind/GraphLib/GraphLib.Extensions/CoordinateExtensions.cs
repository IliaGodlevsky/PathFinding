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

            return self.Juxtapose(coordinate);
        }
    }
}
