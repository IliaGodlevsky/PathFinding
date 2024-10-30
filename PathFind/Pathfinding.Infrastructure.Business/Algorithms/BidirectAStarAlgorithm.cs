using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class BidirectAStarAlgorithm : BidirectDijkstraAlgorithm
    {
        private readonly Dictionary<Coordinate, double> forwardAccumulatedCosts;
        private readonly Dictionary<Coordinate, double> backwardAccumulatedCosts;
        private readonly Dictionary<Coordinate, double> forwardHeuristics;
        private readonly Dictionary<Coordinate, double> backwardHeuristics;
        private readonly IHeuristic heuristic;

        public BidirectAStarAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule(), new ManhattanDistance())
        {
        }

        public BidirectAStarAlgorithm(IEnumerable<IVertex> pathfindingRange,
            IStepRule stepRule, IHeuristic heuristic)
            : base(pathfindingRange, stepRule)
        {
            this.heuristic = heuristic;
            forwardAccumulatedCosts = new();
            backwardAccumulatedCosts = new();
            forwardHeuristics = new();
            backwardHeuristics = new();
        }

        protected override void DropState()
        {
            base.DropState();
            forwardAccumulatedCosts.Clear();
            backwardAccumulatedCosts.Clear();
            forwardHeuristics.Clear();
            backwardHeuristics.Clear();
        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            forwardAccumulatedCosts[Range.Source.Position] = default;
            backwardAccumulatedCosts[Range.Target.Position] = default;
        }

        protected override void EnqueueForward(IVertex vertex, double value)
        {
            if (!forwardHeuristics.TryGetValue(vertex.Position, out double cost))
            {
                cost = CalculateForwardHeuristic(vertex);
                forwardHeuristics[vertex.Position] = cost;
            }
            base.EnqueueForward(vertex, value + cost);
            forwardAccumulatedCosts[vertex.Position] = value;
        }

        protected override void EnqueueBackward(IVertex vertex, double value)
        {
            if (!backwardHeuristics.TryGetValue(vertex.Position, out double cost))
            {
                cost = CalculateBackwardHeuristic(vertex);
                backwardHeuristics[vertex.Position] = cost;
            }
            base.EnqueueBackward(vertex, value + cost);
            backwardAccumulatedCosts[vertex.Position] = value;
        }

        protected override double GetForwardVertexCurrentCost(IVertex vertex)
        {
            return forwardAccumulatedCosts.TryGetValue(vertex.Position, out double cost)
                ? cost
                : double.PositiveInfinity;
        }

        protected override double GetBackwardVertexCurrentCost(IVertex vertex)
        {
            return backwardAccumulatedCosts.TryGetValue(vertex.Position, out double cost)
               ? cost
               : double.PositiveInfinity;
        }

        private double CalculateForwardHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, Range.Target);
        }

        private double CalculateBackwardHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, Range.Source);
        }
    }
}
