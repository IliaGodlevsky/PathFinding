using GraphLib.Interfaces;

namespace GraphLib.Serialization.Extensions
{
    internal static class GraphExtensions
    {
        internal static GraphSerializationInfo ToGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }
    }
}
