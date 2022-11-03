using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Algos.Algos
{
    public class BestFirstLeeAlgorithm : LeeAlgorithm
    {
        private readonly Dictionary<ICoordinate, double> heuristics;
        private readonly IHeuristic heuristic;

        public BestFirstLeeAlgorithm(IEndPoints endPoints, IHeuristic function)
            : base(endPoints)
        {
            heuristic = function;
            heuristics = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
        }

        public BestFirstLeeAlgorithm(IEndPoints endPoints)
            : this(endPoints, new ManhattanDistance())
        {

        }

        protected override IVertex GetNextVertex()
        {
            verticesQueue = verticesQueue
                .OrderBy(v => heuristics[v.Position])
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
            heuristics[vertex.Position] = value + result;
            base.Reevaluate(vertex, value);
        }

        protected override void PrepareForSubPathfinding(Range range)
        {
            base.PrepareForSubPathfinding(range);
            double value = CalculateHeuristic(CurrentRange.Source);
            heuristics[CurrentRange.Source.Position] = value;
        }

        private double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }

        public override string ToString()
        {
            return "Lee algorithm (heusritic)";
        }
    }
}