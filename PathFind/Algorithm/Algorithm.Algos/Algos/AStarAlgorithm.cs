using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic;
using Algorithm.Realizations.StepRules;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using Interruptable.Interface;
using System;

namespace Algorithm.Algos.Algos
{
    /// <summary>
    /// A realization of the A* algorithm
    /// </summary>
    /// <remarks><see cref="https://en.wikipedia.org/wiki/A*_search_algorithm"/></remarks>
    public class AStarAlgorithm : DijkstraAlgorithm, IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
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
