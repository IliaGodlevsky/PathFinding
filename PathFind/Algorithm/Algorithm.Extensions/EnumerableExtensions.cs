using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class EnumerableExtensions
    {
        public static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            return new Queue<T>(collection);
        }
    }
}
