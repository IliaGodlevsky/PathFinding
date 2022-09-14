using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization
{
    public sealed class VertexSerializationInfo
    {
        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public IReadOnlyCollection<ICoordinate> Neighbourhood { get; }

        public VertexSerializationInfo(IVertex vertex)
            : this(vertex.IsObstacle, vertex.Cost, vertex.Position,
                  vertex.Neighbours.Select(v => v.Position).ToArray())
        {

        }

        internal VertexSerializationInfo(bool isObstacle, IVertexCost cost,
            ICoordinate position, IReadOnlyCollection<ICoordinate> neighborhood)
        {
            IsObstacle = isObstacle;
            Cost = cost;
            Position = position;
            Neighbourhood = neighborhood;
        }
    }
}