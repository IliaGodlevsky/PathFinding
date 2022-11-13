using Pathfinding.GraphLib.Core.Interface;
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

        public static async Task VisualizeAsPathAsync<T>(this IEnumerable<T> path)
            where T : IVisualizable
        {
            await Task.Run(() => path.VisualizeAsPath()).ConfigureAwait(false);
        }

        public static void Refresh<T>(this IGraph<T> graph)
            where T : IVertex, IVisualizable
        {
            graph.ForEach(vertex => vertex.RestoreDefaultVisualState());
        }
    }
}
