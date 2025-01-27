using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class GreedyAlgorithm : DepthAlgorithm
    {
        protected GreedyAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected abstract double CalculateGreed(IPathfindingVertex vertex);

        protected override IPathfindingVertex GetVertex(IReadOnlyCollection<IPathfindingVertex> neighbors)
        {
            double leastVertexCost = neighbors.Any() ? neighbors.Min(CalculateGreed) : default;
            return neighbors.FirstOrNullVertex(vertex => CalculateGreed(vertex) == leastVertexCost);
        }
    }
}
