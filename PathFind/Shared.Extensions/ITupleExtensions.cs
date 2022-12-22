using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Shared.Extensions
{
    public static class ITupleExtensions
    {
        public static IEnumerable<T> Enumerate<T>(this ITuple tuple)
        {
            for (int i = 0; i < tuple.Length; i++)
            {
                yield return (T)tuple[i];
            }
        }
    }
}
