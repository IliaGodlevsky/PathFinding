using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class SnakeAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public SnakeAlgorithm(IEnumerable<IVertex> pathfindingRange,
            IHeuristic heuristic)
            : base(pathfindingRange)
        {
            this.heuristic = heuristic;
        }

        public SnakeAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new ManhattanDistance())
        {

        }

        protected override double CalculateGreed(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Source);
        }
    }
}
