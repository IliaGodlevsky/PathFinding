using GraphLib.Interfaces;
using GraphLib.Proxy;
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
            Neighbourhood = new NeighbourhoodProxy(vertex);
        }

        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public INeighborhood Neighbourhood { get; }
    }
}
