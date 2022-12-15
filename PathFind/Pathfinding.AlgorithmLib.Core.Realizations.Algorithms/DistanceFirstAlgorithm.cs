using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal class DistanceFirstAlgorithm : GreedyAlgorithm
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

        protected override double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }

        public override string ToString()
        {
            return Languages.DistanceFirstAlgorithm;
        }
    }
}