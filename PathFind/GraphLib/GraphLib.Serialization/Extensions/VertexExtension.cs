using Common.Extensions;
using GraphLib.Interface;

namespace GraphLib.Serialization.Extensions
{
    public static class VertexExtension
    {
        internal static VertexSerializationInfo GetSerializationInfo(this IVertex self)
        {
            return new VertexSerializationInfo(self);
        }

        public static void Initialize(this IVertex vertex, VertexSerializationInfo info)
        {
            vertex.Position = info.Position.TryCopyDeep();
            vertex.Cost = info.Cost.TryCopyDeep();
            vertex.IsObstacle = info.IsObstacle;
        }
    }
}