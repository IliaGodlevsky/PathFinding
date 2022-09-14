using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Serialization.Extensions
{
    internal static class GraphExtensions
    {
        internal static GraphSerializationInfo ToGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }

        internal static VertexSerializationInfo[] GetVerticesSerializationInfo(this IGraph graph)
        {
            return graph.Select(vertex => vertex.ToSerializationInfo()).ToArray();
        }
    }
}
