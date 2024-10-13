using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class DepthFirstAlgorithm : GreedyAlgorithm
    {
        public DepthFirstAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            return 0;
        }

        protected override IVertex GetNextVertex()
        {
            var neighbors = GetUnvisitedNeighbours(CurrentVertex);
            Enqueued(CurrentVertex, neighbors);
            return neighbors.FirstOrDefault() ?? NullVertex.Interface;
        }
    }
}