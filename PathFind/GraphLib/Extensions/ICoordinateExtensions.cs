using GraphLib.Coordinates.Abstractions;
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
    }
}
