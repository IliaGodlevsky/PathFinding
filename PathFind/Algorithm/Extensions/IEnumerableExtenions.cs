using GraphLib.Extensions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IEnumerableExtenions
    {
        internal static IVertex FirstOrDefault(this IEnumerable<IVertex> collection)
        {
            return !collection.Any() ? new DefaultVertex() : collection.First();
        }

        public static void DrawPath(this IEnumerable<IVertex> path)
        {
            foreach (var vertex in path)
            {
                if (vertex.IsSimpleVertex())
                {
                    vertex.MarkAsPath();
                }
            }
        }
    }
}
