using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using Priority_Queue;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class AStarLeeAlgorithm : BreadthFirstAlgorithm<SimplePriorityQueue<IPathfindingVertex, double>>
    {
        private readonly Dictionary<Coordinate, double> heuristics;
        private readonly IHeuristic heuristic;

        public AStarLeeAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange,
            IHeuristic function)
            : base(pathfindingRange)
        {
            heuristic = function;
            heuristics = new();
        }

        public AStarLeeAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : this(pathfindingRange, new ManhattanDistance())
        {

        }

        protected override void MoveNextVertex()
        {
            CurrentVertex = storage.TryFirstOrThrowDeadEndVertexException();
        }

        protected override void DropState()
        {
            base.DropState();
            storage.Clear();
            heuristics.Clear();
        }

        protected override void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range)
        {
            base.PrepareForSubPathfinding(range);
            double value = CalculateHeuristic(CurrentRange.Source);
            heuristics[CurrentRange.Source.Position] = value;
        }

        protected override void RelaxVertex(IPathfindingVertex vertex)
        {
            if (!heuristics.TryGetValue(vertex.Position, out double cost))
            {
                cost = CalculateHeuristic(vertex);
                heuristics[vertex.Position] = cost;
            }
            storage.Enqueue(vertex, cost);
            base.RelaxVertex(vertex);
        }

        private double CalculateHeuristic(IPathfindingVertex vertex)
        {
            return heuristic.Calculate(vertex, CurrentRange.Target);
        }

        protected override void RelaxNeighbours(IReadOnlyCollection<IPathfindingVertex> neighbours)
        {
            base.RelaxNeighbours(neighbours);
            storage.TryRemove(CurrentVertex);
        }
    }
}