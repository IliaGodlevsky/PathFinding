using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.Visualization.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void VisualizeAsPath<T>(this IEnumerable<T> path)
            where T : IVisualizable
        {
            path.ForEach(vertex => vertex.VisualizeAsPath());
        }

        public static void VisualizeAsObstacles<T>(this IEnumerable<T> path)
            where T : IVisualizable
        {
            path.ForEach(vertex => vertex.VisualizeAsObstacle());
        }

        public static void VisualizeAsRange<T>(this IEnumerable<T> range)
            where T : IVisualizable
        {
            var source = range.FirstOrDefault();
            var target = range.LastOrDefault();
            var intermediates = range.Except(new[] { source, target }).ToArray();
            source?.VisualizeAsSource();
            intermediates.ForEach(item => item.VisualizeAsTransit());
            target?.VisualizeAsTarget();
        }

        public static void VisualizeAsVisited<T>(this IEnumerable<T> visited)
            where T : IVisualizable
        {
            visited.ForEach(item => item.VisualizeAsVisited());
        }

        public static async Task VisualizeAsPathAsync<T>(this IEnumerable<T> path)
            where T : IVisualizable
        {
            await Task.Run(() => path.VisualizeAsPath()).ConfigureAwait(false);
        }
    }
}
