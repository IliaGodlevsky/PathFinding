using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class GraphExtensions
    {
        public static IReadOnlyList<int> GetCosts(this IGraph<Vertex> graph)
        {
            return graph.Select(vertex => vertex.Cost.CurrentCost).ToArray();
        }

        public static void Visualize(this IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                if (vertex.IsObstacle)
                {
                    vertex.VisualizeAsObstacle();
                }
                else
                {
                    vertex.VisualizeAsRegular();
                }
            }
        }
    }
}
