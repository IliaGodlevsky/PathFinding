using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class DistanceFirstAlgorithmFactory : IAlgorithmFactory<DistanceFirstAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public DistanceFirstAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithmFactory()
            : this(new EuclidianDistance())
        {

        }

        public DistanceFirstAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new DistanceFirstAlgorithm(pathfindingRange, heuristic);
        }
    }
}