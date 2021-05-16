using GraphLib.Interfaces;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphExtensions
    {
        internal static GraphSerializationInfo GetGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }
    }
}
