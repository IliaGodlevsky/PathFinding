using GraphLib.Interfaces;

namespace GraphLib.Serialization.Extensions
{
    public static class VertexExtension
    {
        public static void Initialize(this IVertex vertex, VertexSerializationInfo info)
        {
            vertex.Cost = info.Cost;
            vertex.IsObstacle = info.IsObstacle;
        }
    }
}