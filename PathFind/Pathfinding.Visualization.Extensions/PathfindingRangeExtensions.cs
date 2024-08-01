using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;

namespace Pathfinding.Visualization.Extensions
{
    public static class PathfindingRangeExtensions
    {
        public static void RestoreVerticesVisualState<TVertex>(this IPathfindingRange<TVertex> range)
            where TVertex : IVertex, IRangeVisualizable
        {
            range.Source.VisualizeAsSource();
            range.Target.VisualizeAsTarget();
            range.Transit.ForEach(v => v.VisualizeAsTransit());
        }
    }
}
