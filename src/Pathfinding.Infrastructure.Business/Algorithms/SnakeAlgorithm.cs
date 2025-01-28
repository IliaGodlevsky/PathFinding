using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class SnakeAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public SnakeAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange,
            IHeuristic heuristic)
            : base(pathfindingRange)
        {
            this.heuristic = heuristic;
        }

        public SnakeAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : this(pathfindingRange, new ManhattanDistance())
        {

        }

        protected override double CalculateGreed(IPathfindingVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Source);
        }
    }
}
