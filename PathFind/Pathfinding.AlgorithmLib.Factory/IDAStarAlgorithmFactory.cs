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
    [Order(3)]
    [WaveGroup]
    public sealed class IDAStarAlgorithmFactory : IAlgorithmFactory<IDAStarAlgorithm>
    {
        private readonly IStepRule stepRule;
        private readonly IHeuristic heuristic;

        public IDAStarAlgorithmFactory(IStepRule stepRule, IHeuristic heuristic)
        {
            this.heuristic = heuristic;
            this.stepRule = stepRule;
        }

        public IDAStarAlgorithmFactory(IStepRule stepRule)
            : this(stepRule, new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithmFactory(IHeuristic heuristic)
            : this(new DefaultStepRule(), heuristic)
        {

        }

        public IDAStarAlgorithmFactory()
            : this(new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public IDAStarAlgorithm Create(IPathfindingRange pathfindingRange)
        {
            return new IDAStarAlgorithm(pathfindingRange, stepRule, heuristic);
        }

        public override string ToString()
        {
            return "IDA* algorithm";
        }
    }
}