using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(6)]
    [WaveGroup]
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

        public override string ToString()
        {
            return Languages.AStartLeeAlgorithm;
        }
    }
}