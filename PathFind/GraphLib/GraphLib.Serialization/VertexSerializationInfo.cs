using GraphLib.Interfaces;
using System;

namespace GraphLib.Serialization
{
    [Serializable]
    public class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = vertex.Cost;
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
