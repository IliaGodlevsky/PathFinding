using GraphLibrary.Model.Vertex;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Common.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> collection)
        {
            return collection.OrderBy(item => Guid.NewGuid());
        }

        public static IVertex FirstOrDefaultSecure(this IEnumerable<IVertex> collection)
        {
            if (!collection.Any())
                return NullVertex.GetInstance();
            return collection.First();
        }
    }
}
