using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Comparers;
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

        private readonly IEnumerable<IVertex> pathfindingRange;

        protected IVertex Intersection { get; set; } = NullVertex.Interface;

        protected (IVertex Forward, IVertex Backward) Current { get; set; }

        protected (IVertex Source, IVertex Target) Range { get; set; }

        protected BidirectPathfindingAlgorithm(IEnumerable<IVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
            forwardVisited = new HashSet<IVertex>();
            backwardVisited = new HashSet<IVertex>();
            forwardTraces = new Dictionary<Coordinate, IVertex>();
            backwardTraces = new Dictionary<Coordinate, IVertex>();
            Range = (NullVertex.Instance, NullVertex.Interface);
            Current = (NullVertex.Instance, NullVertex.Interface);
        }

        public override sealed IGraphPath FindPath()
        {
            ThrowIfDisposed();
            var subPaths = new List<IGraphPath>();
            foreach (var range in GetSubRanges())
            {
                PrepareForSubPathfinding(range);
                while (!IsDestination())
                {
                    InspectCurrentVertices();
                    Current = GetNextVertices();
                    VisitCurrentVertices();
                }
                var subPath = GetSubPath();
                subPaths.Add(subPath.Forward);
                subPaths.Add(subPath.Backward);
                RaiseSubPathFound(subPath.Forward);
                RaiseSubPathFound(subPath.Backward);
                DropState();
            }
            return new CompositeGraphPath(subPaths);
        }

        protected abstract void InspectCurrentVertices();

        protected abstract (IVertex Forward, IVertex Backward) GetNextVertices();

        protected abstract void VisitCurrentVertices();

        protected virtual bool IsDestination()
        {
            return Intersection != NullVertex.Interface;
        }

        protected virtual void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            Range = range;
            Current = (Range.Source, Range.Target);
        }

        protected virtual (IGraphPath Forward, IGraphPath Backward) GetSubPath()
        {
            var forward = new GraphPath(forwardTraces.ToDictionary(), Intersection);
            var backward = new GraphPath(backwardTraces.ToDictionary(), Intersection);
            return (forward, backward);
        }

        protected virtual void DropState()
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

        private IEnumerable<(IVertex Source, IVertex Target)> GetSubRanges()
        {
            using var iterator = pathfindingRange.GetEnumerator();
            if (iterator.MoveNext())
            {
                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    yield return (previous, current);
                    previous = iterator.Current;
                }
            }
        }
    }
}
