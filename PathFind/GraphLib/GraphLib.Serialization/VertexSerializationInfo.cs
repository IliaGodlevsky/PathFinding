using GraphLib.Interfaces;
using System;

namespace GraphLib.Serialization
{
    [Serializable]
    public sealed class VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = vertex.Cost.Clone();
            Position = vertex.Position.Clone();
            IsObstacle = vertex.IsObstacle;
            NeighboursCoordinates = vertex.Neighborhood.Clone();
        }

        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public INeighborhood NeighboursCoordinates { get; }
    }
}
