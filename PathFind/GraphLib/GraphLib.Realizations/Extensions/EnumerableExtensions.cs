using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static ICoordinate ToCoordinate(this IEnumerable<int> array)
        {
            return new CoordinateProxy(array.ToArray());
        }
    }
}
