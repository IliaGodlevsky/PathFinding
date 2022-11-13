using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Interface.Extensions
{
    public static class IGraphExtensions
    {
        public static IEnumerable<TVertex> GetVertices<TVertex>(this IGraph<TVertex> graph, IGraphPath path)
            where TVertex : IVertex
        {
            return path.Select(graph.Get);
        }
    }
}
