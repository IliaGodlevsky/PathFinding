using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using GraphLib.Interfaces;
using GraphLib.Utility;
using System.Collections.Generic;

namespace Algorithm.Algos.Algos
{
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        private readonly Dictionary<ICoordinate, double> accumulatedCosts;
        protected readonly Dictionary<ICoordinate, double> heuristics;
        protected readonly IHeuristic heuristic;

        public AStarAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarAlgorithm(IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(endPoints, stepRule)
        {
            heuristic = function;
            heuristics = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
            accumulatedCosts = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
        }

        protected override void DropState()
        {
            base.DropState();
            heuristics.Clear();
            accumulatedCosts.Clear();
        }

        protected override void PrepareForSubPathfinding(Range range)
        {
            base.PrepareForSubPathfinding(range);
            accumulatedCosts[CurrentRange.Source.Position] = default;
        }

        protected override void Enqueue(IVertex vertex, double value)
        {
            double heusristicCost = default;
            if (!heuristics.ContainsKey(vertex.Position))
            {
                heusristicCost = CalculateHeuristic(vertex);
                heuristics[vertex.Position] = heusristicCost;
            }
            else
            {
                heusristicCost = heuristics[vertex.Position];
            }
            base.Enqueue(vertex, value + heusristicCost);
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
            return "A * algorithm";
        }
    }
}