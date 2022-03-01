using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Proxy.Extensions
{
    internal static class EnumerableExtensions
    {
        public static ICoordinate ToCoordinate(this IEnumerable<int> array)
        {
            return new CoordinateProxy(array.ToArray());
        }
    }
}
