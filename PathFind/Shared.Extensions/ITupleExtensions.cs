using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Shared.Extensions
{
    public static class ITupleExtensions
    {
        public static IEnumerator GetEnumerator(this ITuple tuple)
        {
            for (int i = 0; i < tuple.Length; i++)
            {
                yield return tuple[i];
            }
        }

        public static IEnumerable AsEnumerable(this ITuple tuple)
        {
            foreach (var item in tuple)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> OfType<T>(this ITuple tuple)
        {
            return tuple.AsEnumerable().OfType<T>();
        }
    }
}
