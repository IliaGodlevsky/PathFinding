using GraphLib.Coordinates.Interface;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class ICoordinateExtensions
    {
        internal static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            if (self?.GetType() != coordinate?.GetType()
                || self?.Coordinates?.Count() != coordinate?.Coordinates?.Count()
                || self?.Coordinates?.Any() == false 
                || coordinate?.Coordinates?.Any() == false) 
            {
                return false;
            }

            for (int i = 0; i < self.Coordinates.Count(); i++)
            {
                if (self.Coordinates.ElementAt(i) != coordinate.Coordinates.ElementAt(i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
