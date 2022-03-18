using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    [DebuggerDisplay("Lee algorithm (heusritic)")]
    [Description("Lee algorithm (heusritic)")]
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        private readonly ICosts heuristics;
        private readonly IHeuristic heuristic;

        public BestFirstLeeAlgorithm(IEndPoints endPoints, IHeuristic function)
            : base(endPoints)
        {
            heuristic = function;
            heuristics = new Costs();
        }

        public BestFirstLeeAlgorithm(IEndPoints endPoints)
            : this(endPoints, new ManhattanDistance())
        {

        }

        protected override IVertex GetNextVertex()
        {
            verticesQueue = verticesQueue
                .OrderBy(heuristics.GetCost)
                .ToQueue();

            return base.GetNextVertex();
        }

        protected override void Reset()
        {
            base.Reset();
            heuristics.Clear();
        }

        protected override void Reevaluate(IVertex vertex, double value)
        {
            double result = CalculateHeuristic(vertex);
            heuristics.Reevaluate(vertex, value + result);
            base.Reevaluate(vertex, value);
        }

        protected override void PrepareForLocalPathfinding()
        {
            base.PrepareForLocalPathfinding();
            double value = CalculateHeuristic(CurrentEndPoints.Source);
            heuristics.Reevaluate(CurrentEndPoints.Source, value);
        }

        private double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentEndPoints.Target);
        }
    }
}