using GraphLib.Interfaces;
using System;

namespace GraphLib.Serialization
{
    [Serializable]
    public class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = (IVertexCost)vertex.Cost.Clone();
            Position = vertex.Position;
            IsObstacle = vertex.IsObstacle;
            NeighboursCoordinates = vertex.NeighboursCoordinates;
        }

        public bool IsObstacle { get; set; }

        public IVertexCost Cost { get; set; }

        public ICoordinate Position { get; set; }

        public INeighboursCoordinates NeighboursCoordinates { get; set; }
    }
}
