using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Extensions
{
    public static class IEnumerableExtension
    {
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> arr)
        {
            return arr.Count() == 0;
        }
    }
}
