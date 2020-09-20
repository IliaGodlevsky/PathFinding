using System;
using System.Collections.Generic;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class IListExtensions
    {
        public static void Apply<TSource>(this IList<TSource> list,
            params Func<TSource, TSource>[] methods)
        {
            for (int x = 0; x < list.Count; x++)
                foreach (var method in methods)
                    list[x] = method(list[x]);            
        }
    }
}
