using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;

namespace GraphLib.Serialization.Extensions
{
    public static class GraphExtensions
    {
        internal static GraphSerializationInfo GetGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }

        internal static IGraph AssembleFrom(this IGraph self,
            in GraphSerializationInfo graphInfo, IVertexFromInfoFactory factory)
        {
            foreach (var info in graphInfo.VerticesInfo)
            {
                self[info.Position] = factory.CreateFrom(info);
            }
            return self;
        }
    }
}
