using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Visualization;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.Service.Interface.Extensions
{
    public static class VisualizationExtensions
    {
        public static void VisualizeAsPath<T>(this IEnumerable<T> path)
            where T : IPathVisualizable
        {
            path.ForEach(vertex => vertex.VisualizeAsPath());
        }

        public static void VisualizeAsRange<T>(this IEnumerable<T> range)
            where T : IRangeVisualizable
        {
            var source = range.FirstOrDefault();
            var target = range.LastOrDefault();
            var intermediates = range.Except(source, target).ToReadOnly();
            source?.VisualizeAsSource();
            intermediates.ForEach(item => item.VisualizeAsTransit());
            target?.VisualizeAsTarget();
        }

        public static async Task VisualizeAsPathAsync<T>(this IEnumerable<T> path)
            where T : ITotallyVisualizable
        {
            await Task.Run(() => path.VisualizeAsPath()).ConfigureAwait(false);
        }

        public static void RestoreVerticesVisualState<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IVertex, IGraphVisualizable
        {
            foreach (var vertex in graph)
            {
                vertex.RestoreDefaultVisualState();
            }
        }

        public static void RestoreDefaultVisualState<TVertex>(this TVertex self)
            where TVertex : IVertex, IGraphVisualizable
        {
            if (self.IsObstacle)
            {
                self.VisualizeAsObstacle();
            }
            else
            {
                self.VisualizeAsRegular();
            }
        }

        public static void RestoreVerticesVisualState<TVertex>(this IPathfindingRange<TVertex> range)
            where TVertex : IVertex, IRangeVisualizable
        {
            range.Source.VisualizeAsSource();
            range.Target.VisualizeAsTarget();
            range.Transit.ForEach(v => v.VisualizeAsTransit());
        }
    }
}
