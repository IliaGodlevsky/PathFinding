using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Service.Interface
{
    public interface IPathfindingVertex
    {
        public bool IsObstacle { get; }

        public Coordinate Position { get; }

        public IVertexCost Cost { get; }

        public IReadOnlyCollection<IPathfindingVertex> Neighbors { get; }
    }
}
