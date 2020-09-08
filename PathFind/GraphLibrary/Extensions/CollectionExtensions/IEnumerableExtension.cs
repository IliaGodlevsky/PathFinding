using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions.CollectionExtensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> collection)
        {
            return collection.OrderBy(item => Guid.NewGuid());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns> if collection is empty returns NullVertex</returns>
        public static IVertex FirstOrDefaultSecure(this IEnumerable<IVertex> collection)
        {
            if (!collection.Any())
                return NullVertex.GetInstance();
            return collection.First();
        }
    }
}
