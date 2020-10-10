using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Vertex;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns> if collection is empty returns NullVertex</returns>
        public static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection)
        {
            return !collection.AsParallel().Any() ? NullVertex.Instance : collection.First();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static void DrawPath(this IEnumerable<IVertex> path)
        {
            Array.ForEach(path.ToArray(), vertex =>
            {
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsPath();
            });
        }
    }
}
