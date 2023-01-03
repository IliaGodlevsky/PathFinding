using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public sealed class VertexSerializationInfo
    {
        private sealed class Neighborhood : INeighborhood
        {
            private readonly IReadOnlyCollection<ICoordinate> coordinates;

            public int Count => coordinates.Count;

            public Neighborhood(IReadOnlyCollection<IVertex> vertices)
                : this(vertices.GetCoordinates().ToArray())
            {
            }

            public Neighborhood(IReadOnlyCollection<ICoordinate> coordinates)
            {
                this.coordinates = coordinates;
            }

            public IEnumerator<ICoordinate> GetEnumerator()
            {
                return coordinates.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public bool IsObstacle { get; }

        public IVertexCost Cost { get; }

        public ICoordinate Position { get; }

        public INeighborhood Neighbourhood { get; }

        public VertexSerializationInfo(IVertex vertex)
            : this(vertex.IsObstacle,
                  vertex.Cost,
                  vertex.Position,
                  vertex.Neighbours.GetCoordinates().ToArray())
        {

        }

        public VertexSerializationInfo(bool isObstacle,
            IVertexCost cost,
            ICoordinate position,
            IReadOnlyCollection<ICoordinate> neighborhood)
        {
            IsObstacle = isObstacle;
            Cost = cost;
            Position = position;
            Neighbourhood = new Neighborhood(neighborhood);
        }
    }
}