using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization.Extensions
{
    internal static class EnumerableExtensions
    {
        public static string ToString(this IEnumerable<char> self)
        {
            return new string(self.ToArray());
        }
    }
}
