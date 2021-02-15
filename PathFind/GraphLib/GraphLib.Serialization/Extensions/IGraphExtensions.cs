using GraphLib.Interface;

namespace GraphLib.Serialization.Extensions
{
    public static class IGraphExtensions
    {
        public static GraphSerializationInfo GetGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }
    }
}
