using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Extensions
{
    public static class EnumerableExtensions
    {
        internal static ICoordinate ToCoordinate(this IEnumerable<int> array)
        {
            return new Coordinate(array.ToArray());
        }
    }
}
