using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Comparers;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using Priority_Queue;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public sealed class AStarLeeAlgorithm : BreadthFirstAlgorithm<SimplePriorityQueue<IVertex, double>>
    {
        private readonly Dictionary<Coordinate, double> heuristics;
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
    }
}