using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using System.ComponentModel;
using System.Diagnostics;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("A* algorithm")]
    [Description("A * algorithm")]
    public class AStarAlgorithm : DijkstraAlgorithm
    {
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
            heuristics.ReevaluateIfNotExists(vertex, CalculateHeuristic);
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

        protected readonly ICosts heuristics;
        protected readonly IHeuristic heuristic;
    }
}
