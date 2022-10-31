using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Visualization.Extensions
{
    internal static class GraphExtensions
    {
        public static void RemoveAllColors<TGraph, TVertex>(this TGraph graph)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex, IVisualizable
        {
            graph
                .OfType<IVisualizable>()
                .ForEach(vertex => vertex.VisualizeAsRegular());
        }

        public static IEnumerable<TVertex> GetObstacles<TGraph, TVertex>(this TGraph graph)
            where TGraph : IGraph<TVertex>
            where TVertex : IVertex, IVisualizable
        {
            return graph.Where(vertex => vertex.IsObstacle);
        }
    }
}
