using GraphLib.Interfaces;
using System;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = vertex.Cost;
            Position = vertex.Position;
            IsObstacle = vertex.IsObstacle;
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICoordinate Position { get; set; }
    }
}
