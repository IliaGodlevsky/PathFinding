using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class DistanceFirstAlgorithm : GreedyAlgorithm
    {
        private readonly IHeuristic heuristic;

        public DistanceFirstAlgorithm(IEnumerable<IVertex> pathfindingRange, IHeuristic heuristic)
            : base(pathfindingRange)
        {
            this.heuristic = heuristic;
        }

        public DistanceFirstAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new EuclidianDistance())
        {

        }

        protected override double CalculateGreed(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }
    }
}