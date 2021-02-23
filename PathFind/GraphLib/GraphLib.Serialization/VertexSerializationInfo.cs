using Common.Extensions;
using GraphLib.Interface;
using System;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = vertex.Cost.TryCopyDeep();
            Position = vertex.Position.TryCopyDeep();
            IsObstacle = vertex.IsObstacle;
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICoordinate Position { get; set; }
    }
}
