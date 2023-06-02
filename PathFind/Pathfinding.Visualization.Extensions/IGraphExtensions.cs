using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.Visualization.Extensions
{
    public static class IGraphExtensions
    {
        public static void RestoreVerticesVisualState<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex, ITotallyVisualizable
        {
            foreach (var vertex in graph)
            {
                vertex.RestoreDefaultVisualState();
            }
        }
    }
}