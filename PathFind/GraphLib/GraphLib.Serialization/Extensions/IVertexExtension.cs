using Common.Extensions;
using GraphLib.Interface;

namespace GraphLib.Serialization.Extensions
{
    public static class IVertexExtension
    {
        internal static VertexSerializationInfo GetSerializationInfo(this IVertex self)
        {
            return new VertexSerializationInfo(self);
        }

        public static void Initialize(this IVertex vertex, VertexSerializationInfo info)
        {
            vertex.Position = info.Position.DeepCopy();
            vertex.Cost = info.Cost.DeepCopy();
            vertex.IsObstacle = info.IsObstacle;
        }
    }
}