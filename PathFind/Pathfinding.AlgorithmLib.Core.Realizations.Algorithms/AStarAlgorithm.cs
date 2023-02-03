using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Core.Realizations.StepRules;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal class AStarAlgorithm : DijkstraAlgorithm
    {
        private readonly Dictionary<ICoordinate, double> accumulatedCosts;
        protected readonly Dictionary<ICoordinate, double> heuristics;
        protected readonly IHeuristic heuristic;

        public AStarAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarAlgorithm(IEnumerable<IVertex> pathfindingRange, IStepRule stepRule, IHeuristic function)
            : base(pathfindingRange, stepRule)
        {
            heuristic = function;
            heuristics = new (new CoordinateEqualityComparer());
            accumulatedCosts = new (new CoordinateEqualityComparer());
        }

        protected override void DropState()
        {
            base.DropState();
            heuristics.Clear();
            accumulatedCosts.Clear();
        }

        protected override void PrepareForSubPathfinding(SubRange range)
        {
            base.PrepareForSubPathfinding(range);
            accumulatedCosts[CurrentRange.Source.Position] = default;
        }

        protected override void Enqueue(IVertex vertex, double value)
        {
            double cost = default;
            if (!heuristics.TryGetValue(vertex.Position, out cost))
            {
                cost = CalculateHeuristic(vertex);
                heuristics[vertex.Position] = cost;
            }
            base.Enqueue(vertex, value + cost);
            accumulatedCosts[vertex.Position] = value;
        }

        protected override double GetVertexCurrentCost(IVertex vertex)
        {
            return accumulatedCosts.TryGetValue(vertex.Position, out double cost) ? cost : double.PositiveInfinity;
        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }

        public override string ToString()
        {
            return Languages.AStartAlgorithm;
        }
    }
}