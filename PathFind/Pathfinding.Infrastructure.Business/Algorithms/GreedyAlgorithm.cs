using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class GreedyAlgorithm : DepthAlgorithm
    {
        protected GreedyAlgorithm(IEnumerable<IVertex> pathfindingRange) 
            : base(pathfindingRange)
        {
        }

        protected abstract double CalculateGreed(IVertex vertex);

        protected override IVertex GetVertex(IReadOnlyCollection<IVertex> neighbors)
        {
            double leastVertexCost = neighbors.Any() ? neighbors.Min(CalculateGreed) : default;
            return neighbors.FirstOrNullVertex(vertex => CalculateGreed(vertex) == leastVertexCost);
        }
    }
}
