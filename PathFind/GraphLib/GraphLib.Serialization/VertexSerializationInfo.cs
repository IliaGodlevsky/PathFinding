using GraphLib.Interfaces;
using GraphLib.Proxy;

namespace GraphLib.Serialization
{
    public sealed class VertexSerializationInfo
    {
        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public INeighborhood Neighbourhood { get; }

        public VertexSerializationInfo(IVertex vertex)
            : this(vertex.IsObstacle, vertex.Cost, vertex.Position, new NeighbourhoodProxy(vertex))
        {

        }

        internal VertexSerializationInfo(bool isObstacle, IVertexCost cost,
            ICoordinate position, INeighborhood neighborhood)
        {
            IsObstacle = isObstacle;
            Cost = cost;
            Position = position;
            Neighbourhood = neighborhood;
        }
    }
}