using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GraphLib.Realizations.Extensions
{
    internal static class EnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ICoordinate ToCoordinate(this IEnumerable<int> array)
        {
            return new CoordinateProxy(array.ToArray());
        }
    }
}
