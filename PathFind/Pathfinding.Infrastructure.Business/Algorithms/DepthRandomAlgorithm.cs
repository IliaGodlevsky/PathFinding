using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Interface;
using Pathfinding.Shared.Random;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class DepthRandomAlgorithm : DepthAlgorithm
    {
        private readonly IRandom random;

        public DepthRandomAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new XorshiftRandom())
        {
        }

        public DepthRandomAlgorithm(IEnumerable<IVertex> pathfindingRange, IRandom random)
            : base(pathfindingRange)
        {
            this.random = random;
        }

        protected override IVertex GetVertex(IReadOnlyCollection<IVertex> neighbors)
        {
            if (neighbors.Count > 0)
            {
                int limit = neighbors.Count - 1;
                int index = random.NextInt(limit);
                return neighbors.ElementAtOrDefault(index);
            }
            return NullVertex.Interface;
        }
    }
}
