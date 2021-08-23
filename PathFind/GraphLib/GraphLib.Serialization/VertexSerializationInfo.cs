using GraphLib.Interfaces;
using System;

namespace GraphLib.Serialization
{
    [Serializable]
    public readonly struct VertexSerializationInfo
    {
        public VertexSerializationInfo(IVertex vertex)
        {
            Cost = vertex.Cost.Clone();
            Position = vertex.Position;
            IsObstacle = vertex.IsObstacle;
            NeighboursCoordinates = vertex.NeighboursCoordinates;
        }

        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public INeighboursCoordinates NeighboursCoordinates { get; }
    }
}
