using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Interfaces;
using Algorithm.Realizations.Heuristic.Distances;
using GraphLib.Interfaces;
using GraphLib.Utility;
using Priority_Queue;
using System.Collections.Generic;

namespace Algorithm.Algos.Algos
{
    public sealed class AStarLeeAlgorithm : BreadthFirstAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        private readonly Dictionary<ICoordinate, double> heuristics;
        private readonly IHeuristic heuristic;

        public AStarLeeAlgorithm(IEndPoints endPoints, IHeuristic function)
            : base(endPoints)
        {
            heuristic = function;
            heuristics = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
        }

        public AStarLeeAlgorithm(IEndPoints endPoints)
            : this(endPoints, new ManhattanDistance())
        {

        }

        protected override IVertex GetNextVertex()
        {
            return storage.TryFirstOrDeadEndVertex();
        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
            heuristics.Clear();
        }

        protected override void PrepareForSubPathfinding(Range range)
        {
            base.PrepareForSubPathfinding(range);
            double value = CalculateHeuristic(CurrentRange.Source);
            heuristics[CurrentRange.Source.Position] = value;
        }

        protected override void RelaxVertex(IVertex vertex)
        {
            double cost = default;
            if (!heuristics.TryGetValue(vertex.Position, out cost))
            {
                cost = CalculateHeuristic(vertex);
                heuristics[vertex.Position] = cost;
            }
            storage.Enqueue(vertex, cost);
            base.RelaxVertex(vertex);
        }

        private double CalculateHeuristic(IVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IVertex> neighbours)
        {
            base.RelaxNeighbours(neighbours);
            storage.TryRemove(CurrentVertex);
        }

        public override string ToString()
        {
            return "A* lee algorithm";
        }
    }
}