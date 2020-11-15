using GraphLib.Coordinates.Interface;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class ICoordinateExtensions
    {
        internal static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            if (self == null || coordinate == null)
            {
                return false;
            }

            return !self.Coordinates.Except(coordinate.Coordinates).Any();
        }
    }
}
