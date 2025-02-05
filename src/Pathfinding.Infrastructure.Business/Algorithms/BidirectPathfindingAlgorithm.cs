﻿using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Immutable;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BidirectPathfindingAlgorithm<TStorage>(IEnumerable<IPathfindingVertex> pathfindingRange) 
        : PathfindingProcess(pathfindingRange)
        where TStorage : new()
    {
        protected readonly TStorage forwardStorage = new();
        protected readonly TStorage backwardStorage = new();
        protected readonly HashSet<IPathfindingVertex> forwardVisited = [];
        protected readonly HashSet<IPathfindingVertex> backwardVisited = [];
        protected readonly Dictionary<Coordinate, IPathfindingVertex> forwardTraces = [];
        protected readonly Dictionary<Coordinate, IPathfindingVertex> backwardTraces = [];

        protected IPathfindingVertex Intersection { get; set; } = NullPathfindingVertex.Interface;

        protected (IPathfindingVertex Forward, IPathfindingVertex Backward) Current { get; set; } = (NullPathfindingVertex.Instance, NullPathfindingVertex.Interface);

        protected (IPathfindingVertex Source, IPathfindingVertex Target) Range { get; set; } = (NullPathfindingVertex.Instance, NullPathfindingVertex.Interface);

        protected override bool IsDestination()
        {
            return Intersection != NullPathfindingVertex.Interface;
        }

        protected override void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range)
        {
            Range = range;
            Current = (Range.Source, Range.Target);
        }

        protected override IGraphPath GetSubPath()
        {
            return new BidirectGraphPath(
                forwardTraces.ToImmutableDictionary(),
                backwardTraces.ToImmutableDictionary(), Intersection);
        }

        protected override void DropState()
        {
            forwardVisited.Clear();
            backwardVisited.Clear();
            forwardTraces.Clear();
            backwardTraces.Clear();
            Intersection = NullPathfindingVertex.Interface;
        }

        protected virtual IReadOnlyCollection<IPathfindingVertex> GetForwardUnvisitedNeighbours()
        {
            return GetUnvisitedNeighbours(Current.Forward, forwardVisited);
        }

        protected virtual IReadOnlyCollection<IPathfindingVertex> GetBackwardUnvisitedNeighbours()
        {
            return GetUnvisitedNeighbours(Current.Backward, backwardVisited);
        }

        private static IPathfindingVertex[] GetUnvisitedNeighbours(IPathfindingVertex vertex,
            HashSet<IPathfindingVertex> visited)
        {
            return vertex.Neighbors
                .Where(v => !v.IsObstacle && !visited.Contains(v))
                .ToArray();
        }
    }
}
