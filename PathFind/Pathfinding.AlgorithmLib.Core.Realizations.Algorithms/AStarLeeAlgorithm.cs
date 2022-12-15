using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Realizations.Algorithms.Localization;
using Pathfinding.AlgorithmLib.Core.Realizations.Heuristics;
using Pathfinding.AlgorithmLib.Extensions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Priority_Queue;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Algorithms
{
    internal sealed class AStarLeeAlgorithm : BreadthFirstAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        private readonly Dictionary<ICoordinate, double> heuristics;
        private readonly IHeuristic heuristic;

        public AStarLeeAlgorithm(IEnumerable<IVertex> pathfindingRange, IHeuristic function)
            : base(pathfindingRange)
        {
            heuristic = function;
            heuristics = new Dictionary<ICoordinate, double>(new CoordinateEqualityComparer());
        }

        public AStarLeeAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new ManhattanDistance())
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

        protected override void PrepareForSubPathfinding(SubRange range)
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
            return Languages.AStartLeeAlgorithm;
        }
    }
}