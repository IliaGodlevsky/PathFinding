using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization.Extensions
{
    internal static class GraphExtensions
    {
        internal static GraphSerializationInfo ToGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }

        internal static IReadOnlyCollection<VertexSerializationInfo> GetVerticesSerializationInfo(this IGraph graph)
        {
            return graph.Select(vertex => vertex.ToSerializationInfo()).ToReadOnly();
        }
    }
}
