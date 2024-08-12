using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Comparers;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        private readonly Dictionary<Coordinate, double> accumulatedCosts;
        protected readonly Dictionary<Coordinate, double> heuristics;
        protected readonly IHeuristic heuristic;

        public AStarAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarAlgorithm(IEnumerable<IVertex> pathfindingRange, IStepRule stepRule, IHeuristic function)
            : base(pathfindingRange, stepRule)
        {
            heuristic = function;
            heuristics = new(CoordinateEqualityComparer.Interface);
            accumulatedCosts = new(CoordinateEqualityComparer.Interface);
        }

        protected override void DropState()
        {
            base.DropState();
            heuristics.Clear();
            accumulatedCosts.Clear();
        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            accumulatedCosts[CurrentRange.Source.Position] = default;
        }

        protected override void Enqueue(IVertex vertex, double value)
        {
            if (!heuristics.TryGetValue(vertex.Position, out double cost))
            {
                cost = CalculateHeuristic(vertex);
                heuristics[vertex.Position] = cost;
            }
            base.Enqueue(vertex, value + cost);
            accumulatedCosts[vertex.Position] = value;
        }

        protected override double GetVertexCurrentCost(IVertex vertex)
        {
            return accumulatedCosts.TryGetValue(vertex.Position, out double cost)
                ? cost
                : double.PositiveInfinity;
        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }
    }
}