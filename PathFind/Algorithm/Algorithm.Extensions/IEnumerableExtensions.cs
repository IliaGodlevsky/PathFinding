using Algorithm.Сompanions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Extensions
{
    public static class IEnumerableExtensions
    {
        public static Dictionary<ICoordinate, Node> ToDictionary(this IGraph graph)
        {
            return graph.Vertices
                .Select(vertex => new Node(vertex))
                .ToDictionary(node => node.Vertex.Position);
        }
    }
}
