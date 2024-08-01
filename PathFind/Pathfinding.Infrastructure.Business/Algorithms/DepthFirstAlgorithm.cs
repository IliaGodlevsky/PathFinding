using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class DepthFirstAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public DepthFirstAlgorithm(IEnumerable<IVertex> pathfindingRange, IHeuristic heuristic)
            : base(pathfindingRange)
        {
            this.heuristic = heuristic;
        }

        public DepthFirstAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new ManhattanDistance())
        {

        }

        protected override double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Source);
        }
    }
}