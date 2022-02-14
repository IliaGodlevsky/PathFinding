using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GraphLib.Proxy.Extensions
{
    internal static class EnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICoordinate ToCoordinate(this IEnumerable<int> array)
        {
            return new CoordinateProxy(array.ToArray());
        }
    }
}
