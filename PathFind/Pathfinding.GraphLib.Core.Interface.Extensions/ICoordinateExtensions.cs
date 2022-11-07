using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class ICoordinateExtensions
    {
        public static bool IsCardinal(this ICoordinate self, ICoordinate cardinalCoordinates)
        {
            // Cardinal coordinate differs from central coordinate only for one coordinate value
            return self.Count == cardinalCoordinates.Count
                ? self.Zip(cardinalCoordinates, (x, y) => x != y).Count(i => i) == 1
                : false;
        }
    }
}
