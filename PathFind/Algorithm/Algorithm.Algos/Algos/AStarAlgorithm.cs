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
        }

        protected override void Reset()
        {
            base.Reset();
            heuristics.Clear();
        }

        protected override void Enqueue(IVertex vertex, double value)
        {
            if (!heuristics.Contains(vertex))
            {
                heuristics.Reevaluate(vertex, CalculateHeuristic(vertex));
            }
            base.Enqueue(vertex, value + heuristics.GetCost(vertex));
        }

        protected override double GetVertexCurrentCost(IVertex vertex)
        {
            return base.GetVertexCurrentCost(vertex)
                - heuristics.GetCostOrDefault(vertex, default);
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