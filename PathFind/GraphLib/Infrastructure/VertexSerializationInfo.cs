using GraphLib.Interface;
using GraphLib.VertexCost;
using System;

namespace GraphLib.Infrastructure
{
    [Serializable]
    public sealed class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            IsObstacle = vertex.IsObstacle;
            Cost = vertex.Cost;
            Position = (ICoordinate)vertex.Position.Clone();
        }

        public bool IsObstacle { get; set; }

        public Cost Cost { get; set; }

        public ICoordinate Position { get; set; }
    }
}
