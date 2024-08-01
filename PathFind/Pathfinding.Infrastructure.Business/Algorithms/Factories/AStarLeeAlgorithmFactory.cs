using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms.Factories
{
    public sealed class AStarLeeAlgorithmFactory : IAlgorithmFactory<AStarLeeAlgorithm>
    {
        private readonly IHeuristic heuristic;

        public AStarLeeAlgorithmFactory(IHeuristic heuristic)
        {
            this.heuristic = heuristic;
        }

        public AStarLeeAlgorithmFactory()
            : this(new ManhattanDistance())
        {

        }

        public AStarLeeAlgorithm Create(IEnumerable<IVertex> pathfindingRange)
        {
            return new AStarLeeAlgorithm(pathfindingRange, heuristic);
        }
    }
}