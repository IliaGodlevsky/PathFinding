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
    public sealed class AStarLeeAlgorithm : BreadthFirstAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        private readonly Dictionary<ICoordinate, double> heuristics;
        private readonly IHeuristic heuristic;

        public AStarLeeAlgorithm(IEnumerable<IVertex> pathfindingRange, IHeuristic function)
            : base(pathfindingRange)
        {
            heuristic = function;
            heuristics = new(CoordinateEqualityComparer.Interface);
        }

        public AStarLeeAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : this(pathfindingRange, new ManhattanDistance())
        {

        }

        protected override IVertex GetNextVertex()
        {
            return storage.TryFirstOrThrowDeadEndVertexException();
        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
            heuristics.Clear();
        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            double value = CalculateHeuristic(CurrentRange.Source);
            heuristics[CurrentRange.Source.Position] = value;
        }

        protected override void RelaxVertex(IVertex vertex)
        {
            if (!heuristics.TryGetValue(vertex.Position, out double cost))
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