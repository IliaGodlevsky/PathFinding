using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class BidirectAStarAlgorithm : BidirectDijkstraAlgorithm
    {
        private readonly Dictionary<Coordinate, double> forwardAccumulatedCosts;
        private readonly Dictionary<Coordinate, double> backwardAccumulatedCosts;
        private readonly Dictionary<Coordinate, double> forwardHeuristics;
        private readonly Dictionary<Coordinate, double> backwardHeuristics;
        private readonly IHeuristic heuristic;

        public BidirectAStarAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule(), new ManhattanDistance())
        {
        }

        public BidirectAStarAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange,
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

        protected override void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            forwardAccumulatedCosts[Range.Source.Position] = default;
            backwardAccumulatedCosts[Range.Target.Position] = default;
        }

        protected override void EnqueueForward(IPathfindingVertex vertex, double value)
        {
            if (!forwardHeuristics.TryGetValue(vertex.Position, out double cost))
            {
                cost = CalculateForwardHeuristic(vertex);
                forwardHeuristics[vertex.Position] = cost;
            }
            base.EnqueueForward(vertex, value + cost);
            forwardAccumulatedCosts[vertex.Position] = value;
        }

        protected override void EnqueueBackward(IPathfindingVertex vertex, double value)
        {
            if (!backwardHeuristics.TryGetValue(vertex.Position, out double cost))
            {
                cost = CalculateBackwardHeuristic(vertex);
                backwardHeuristics[vertex.Position] = cost;
            }
            base.EnqueueBackward(vertex, value + cost);
            backwardAccumulatedCosts[vertex.Position] = value;
        }

        protected override double GetForwardVertexCurrentCost(IPathfindingVertex vertex)
        {
            return forwardAccumulatedCosts.TryGetValue(vertex.Position, out double cost)
                ? cost
                : double.PositiveInfinity;
        }

        protected override double GetBackwardVertexCurrentCost(IPathfindingVertex vertex)
        {
            return backwardAccumulatedCosts.TryGetValue(vertex.Position, out double cost)
               ? cost
               : double.PositiveInfinity;
        }

        private double CalculateForwardHeuristic(IPathfindingVertex vertex)
        {
            return heuristic.Calculate(vertex, Range.Target);
        }

        private double CalculateBackwardHeuristic(IPathfindingVertex vertex)
        {
            return heuristic.Calculate(vertex, Range.Source);
        }
    }
}
