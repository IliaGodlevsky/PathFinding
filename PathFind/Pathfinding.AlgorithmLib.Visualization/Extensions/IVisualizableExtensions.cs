using Pathfinding.VisualizationLib.Core.Interface;
using System;

namespace Pathfinding.AlgorithmLib.Visualization.Extensions
{
    internal static class IVisualizableExtensions
    {
        public static bool TryVisualizeAsVisited(this IVisualizable vertex) => vertex.TryVisualize(vis => vis.VisualizeAsVisited());

        public static bool TryVisualizeAsEnqueued(this IVisualizable vertex) => vertex.TryVisualize(vis => vis.VisualizeAsEnqueued());

        private static bool TryVisualize(this IVisualizable vertex, Action<IVisualizable> action)
        {
            if (!vertex.IsVisualizedAsEndPoint())
            {
                action(vertex);
                return true;
            }

            return false;
        }
    }
}
