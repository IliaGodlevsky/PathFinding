using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;

namespace Pathfinding.Visualization.Extensions
{
    public static class IGraphExtensions
    {
        public static void RestoreVerticesVisualState<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex, IVisualizable
        {
            graph.ForEach(vertex => vertex.RestoreDefaultVisualState());
        }
    }
}