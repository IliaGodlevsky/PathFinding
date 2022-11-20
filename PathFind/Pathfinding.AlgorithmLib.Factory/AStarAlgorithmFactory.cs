using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.AlgorithmLib.Factory.Attrbiutes;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.AlgorithmLib.Factory
{
    [Order(2)]
    [WaveGroup]
    public sealed class AStarAlgorithmFactory : IAlgorithmFactory<PathfindingProcess>
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public AStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public AStarAlgorithmFactory(IStepRule stepRule)
            : this(stepRule, new ChebyshevDistance())
        {

        }

        public AStarAlgorithmFactory(IHeuristic heuristic)
            : this(new DefaultStepRule(), heuristic)
        {

        }

        public AStarAlgorithmFactory()
            : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public PathfindingProcess Create(IPathfindingRange pathfindingRange)
        {
            return new AStarAlgorithm(pathfindingRange, stepRule, heuristic);
        }

        public override string ToString()
        {
            return "A* algorithm";
        }
    }
}