using Common.Extensions;
using GraphLib.Interface;
using System;

namespace GraphLib.Infrastructure
{
    [Serializable]
    public sealed class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = vertex.Cost.DeepCopy();
            Position = vertex.Position.DeepCopy();
            IsObstacle = vertex.IsObstacle;
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICoordinate Position { get; set; }
    }
}
