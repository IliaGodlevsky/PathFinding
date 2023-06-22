using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using Shared.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class VisualizationSetExtensions
    {
        public static void Visualize(this VisualizationSet set, Graph2D<Vertex> graph)
        {
            set.Obstacles.Select(graph.Get).ForEach(v => v.VisualizeAsObstacle());
            set.Visited.Select(graph.Get).ForEach(v => v.VisualizeAsVisited());
            set.Range.Reverse().Select(graph.Get).VisualizeAsRange();
            set.Path.Select(graph.Get).VisualizeAsPath();
        }
    }
}
