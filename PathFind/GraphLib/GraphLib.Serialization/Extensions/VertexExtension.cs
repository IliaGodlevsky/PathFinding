using Common.Extensions;
using GraphLib.Interfaces;

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
            vertex.Cost = info.Cost.TryCopyDeep();
            vertex.IsObstacle = info.IsObstacle;
        }
    }
}