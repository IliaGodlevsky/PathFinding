using GraphLib.Extensions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IEnumerableExtenions
    {
        internal static IVertex FirstOrNullVertex(this IEnumerable<IVertex> collection)
        {
            return !collection.AsParallel().Any() ? NullVertex.Instance : collection.First();
        }

        public static void DrawPath(this IEnumerable<IVertex> path)
        {
            foreach (var vertex in path)
            {
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsPath();
            }
        }
    }
}
