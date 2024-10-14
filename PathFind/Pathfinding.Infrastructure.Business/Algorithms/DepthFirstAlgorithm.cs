using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class DepthFirstAlgorithm : DepthAlgorithm
    {
        public DepthFirstAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        protected override IVertex GetVertex(IReadOnlyCollection<IVertex> neighbors)
        {
            return neighbors.FirstOrDefault() ?? NullVertex.Interface;
        }
    }
}