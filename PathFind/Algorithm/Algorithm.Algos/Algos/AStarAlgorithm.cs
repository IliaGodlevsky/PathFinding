using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;

namespace Algorithm.Algos.Algos
{
    public class AStarAlgorithm : DijkstraAlgorithm
    {
        private readonly ICosts accumulatedCosts;
        protected readonly ICosts heuristics;
        protected readonly IHeuristic heuristic;

        public AStarAlgorithm(IEndPoints endPoints)
            : this(endPoints, new DefaultStepRule(), new ChebyshevDistance())
        {

        }

        public AStarAlgorithm(IEndPoints endPoints, IStepRule stepRule, IHeuristic function)
            : base(endPoints, stepRule)
        {
            heuristic = function;
            heuristics = new Costs();
            accumulatedCosts = new Costs();
        }

        protected override void Reset()
        {
            base.Reset();
            heuristics.Clear();
            accumulatedCosts.Clear();
        }

        protected override void PrepareForLocalPathfinding(IEndPoints endPoints)
        {
            base.PrepareForLocalPathfinding(endPoints);
            accumulatedCosts.Reevaluate(CurrentEndPoints.Source, default);
        }

        protected override void Enqueue(IVertex vertex, double value)
        {
            double heusristicCost = default;
            if (!heuristics.Contains(vertex))
            {
                heusristicCost = CalculateHeuristic(vertex);
                heuristics.Reevaluate(vertex, heusristicCost);
            }
            else
            {
                heusristicCost = heuristics.GetCost(vertex);
            }
            base.Enqueue(vertex, value + heusristicCost);
            accumulatedCosts.Reevaluate(vertex, value);
        }

        protected override double GetVertexCurrentCost(IVertex vertex)
        {
            return accumulatedCosts.GetCostOrDefault(vertex);
        }

        protected virtual double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentEndPoints.Target);
        }

        public override string ToString()
        {
            return "A * algorithm";
        }
    }
}