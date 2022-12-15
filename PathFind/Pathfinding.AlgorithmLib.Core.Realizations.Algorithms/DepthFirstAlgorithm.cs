using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.GraphLib.Core.Interface;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal sealed class DepthFirstAlgorithm : GreedyAlgorithm
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

        public override string ToString()
        {
            return Languages.DepthFirstAlgorithm;
        }
    }
}