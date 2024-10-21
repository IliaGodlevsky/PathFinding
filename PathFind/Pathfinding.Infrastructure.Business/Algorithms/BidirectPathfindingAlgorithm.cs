using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class BidirectPathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected readonly TStorage forwardStorage = new();
        protected readonly TStorage backwardStorage = new();
        protected readonly HashSet<IVertex> forwardVisited;
        protected readonly HashSet<IVertex> backwardVisited;
        protected readonly Dictionary<Coordinate, IVertex> forwardTraces;
        protected readonly Dictionary<Coordinate, IVertex> backwardTraces;

        protected IVertex Intersection { get; set; } = NullVertex.Interface;

        protected (IVertex Forward, IVertex Backward) Current { get; set; }

        protected (IVertex Source, IVertex Target) Range { get; set; }

        protected BidirectPathfindingAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            forwardVisited = new HashSet<IVertex>();
            backwardVisited = new HashSet<IVertex>();
            forwardTraces = new Dictionary<Coordinate, IVertex>();
            backwardTraces = new Dictionary<Coordinate, IVertex>();
            Range = (NullVertex.Instance, NullVertex.Interface);
            Current = (NullVertex.Instance, NullVertex.Interface);
        }

        protected override bool IsDestination()
        {
            return Intersection != NullVertex.Interface;
        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            Range = range;
            Current = (Range.Source, Range.Target);
        }

        protected override IGraphPath GetSubPath()
        {
            return new BidirectGraphPath(forwardTraces.ToDictionary(),
                backwardTraces.ToDictionary(), Intersection);
        }

        protected override void DropState()
        {
            forwardVisited.Clear();
            backwardVisited.Clear();
            forwardTraces.Clear();
            backwardTraces.Clear();
            Intersection = NullVertex.Interface;
        }

        protected virtual IReadOnlyCollection<IVertex> GetForwardUnvisitedNeighbours()
        {
            return GetUnvisitedNeighbours(Current.Forward, forwardVisited);
        }

        protected virtual IReadOnlyCollection<IVertex> GetBackwardUnvisitedNeighbours()
        {
            return GetUnvisitedNeighbours(Current.Backward, backwardVisited);
        }

        private IReadOnlyCollection<IVertex> GetUnvisitedNeighbours(IVertex vertex, HashSet<IVertex> visited)
        {
            return vertex.Neighbours
                .Where(v => !v.IsObstacle && !visited.Contains(v))
                .ToArray();
        }
    }
}
