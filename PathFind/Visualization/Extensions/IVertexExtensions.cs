using GraphLib.Interfaces;
using System;

namespace Visualization.Extensions
{
    internal static class IVertexExtensions
    {
        public static bool TryVisualizeAsVisited(this IVisualizable vertex) => vertex.TryVisualize(vis => vis.VisualizeAsVisited());

        public static bool TryVisualizeAsEnqueued(this IVisualizable vertex) => vertex.TryVisualize(vis => vis.VisualizeAsEnqueued());

        private static bool TryVisualize(this IVisualizable vertex, Action<IVisualizable> action)
        {
            if (!vertex.IsVisualizedAsEndPoint)
            {
                action(vertex);
                return true;
            }

            return false;
        }
    }
}
