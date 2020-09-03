using System;
using System.Collections.Generic;

namespace GraphLibrary.Common.Extensions.CollectionExtensions
{
    public static class ListExtension
    {
        public static void Apply<TSource>(this List<TSource> list,
            params Func<TSource, TSource>[] methods)
        {
            for (int x = 0; x < list.Count; x++)
                foreach (var method in methods)
                    list[x] = method(list[x]);
        }
    }
}
