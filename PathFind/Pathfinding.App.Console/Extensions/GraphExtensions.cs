using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class GraphExtensions
    {
        public static IReadOnlyList<int> GetCosts(this IGraph<Vertex> graph)
        {
            return graph.Select(vertex => vertex.Cost.CurrentCost).ToReadOnly();
        }
    }
}
