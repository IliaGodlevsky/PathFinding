using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Visualization.Extensions
{
    internal static class GraphExtensions
    {
        public static void RemoveAllColors(this IGraph graph)
        {
            graph
                .OfType<IVisualizable>()
                .ForEach(vertex => vertex.VisualizeAsRegular());
        }

        public static IEnumerable<IVertex> GetObstacles(this IGraph graph)
        {
            return graph.Where(vertex => vertex.IsObstacle);
        }
    }
}
