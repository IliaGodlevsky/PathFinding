using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class DepthFirstAlgorithmFactory : IAlgorithmFactory<DepthFirstAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public DepthFirstAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithmFactory()
            : this(new ManhattanDistance())
        {

        }

        public DepthFirstAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new DepthFirstAlgorithm(pathfindingRange, heuristic);
        }
    }
}