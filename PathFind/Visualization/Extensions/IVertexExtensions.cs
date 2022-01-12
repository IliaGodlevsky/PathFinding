using GraphLib.Interfaces;
using System;

namespace Visualization.Extensions
{
    internal static class IVertexExtensions
    {
        public static bool TryVisualizeAsVisited(this IVertex vertex) => vertex.TryVisualize(vis => vis.VisualizeAsVisited());

        public static bool TryVisualizeAsEnqueued(this IVertex vertex) => vertex.TryVisualize(vis => vis.VisualizeAsEnqueued());

        private static bool TryVisualize(this IVertex vertex, Action<IVisualizable> action)
        {
            if (vertex is IVisualizable vert && !vert.IsVisualizedAsEndPoint)
            {
                action(vert);
                return true;
            }

            return false;
        }
    }
}
