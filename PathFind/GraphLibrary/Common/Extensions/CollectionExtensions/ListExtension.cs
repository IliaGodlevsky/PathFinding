using GraphLibrary.Model.Vertex;
using GraphLibrary.Vertex;
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

        public static IVertex FindSecure(this List<IVertex> list, Predicate<IVertex> match)
        {
            if (list.Find(match) == null)
                return NullVertex.GetInstance();
            return list.Find(match);
        }
    }
}
