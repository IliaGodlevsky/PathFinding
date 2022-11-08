using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    public static class IVertexExtension
    {
        public static void Initialize(this IVertex vertex, VertexSerializationInfo info)
        {
            vertex.Cost = info.Cost;
            vertex.IsObstacle = info.IsObstacle;
        }
    }
}