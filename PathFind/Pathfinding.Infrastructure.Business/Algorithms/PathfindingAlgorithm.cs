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
    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected readonly IEnumerable<IVertex> pathfindingRange;
        protected readonly HashSet<IVertex> visited
            = new(VertexEqualityComparer.Interface);
        protected readonly Dictionary<Coordinate, IVertex> traces
            = new(CoordinateEqualityComparer.Interface);
        protected readonly TStorage storage = new();

        protected (IVertex Source, IVertex Target) CurrentRange { get; set; }

        protected IVertex CurrentVertex { get; set; } = NullVertex.Interface;

        protected PathfindingAlgorithm(IEnumerable<IVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
            CurrentRange = (NullVertex.Interface, NullVertex.Interface);
        }

        public sealed override IGraphPath FindPath()
        {
            ThrowIfDisposed();
            var subPaths = new List<IGraphPath>();
            foreach (var range in GetSubRanges())
            {
                PrepareForSubPathfinding(range);
                while (!IsDestination())
                {
                    InspectCurrentVertex();
                    CurrentVertex = GetNextVertex();
                    VisitCurrentVertex();
                }
                var subPath = GetSubPath();
                subPaths.Add(subPath);
                RaiseSubPathFound(subPath);
                DropState();
            }
            return CreatePath(subPaths);
        }

        public override void Dispose()
        {
            base.Dispose();
            DropState();
        }

        protected abstract IVertex GetNextVertex();

        protected abstract void InspectCurrentVertex();

        protected abstract void VisitCurrentVertex();

        protected virtual bool IsDestination()
        {
            return CurrentVertex.Equals(CurrentRange.Target);
        }

        protected virtual void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            CurrentRange = range;
            CurrentVertex = CurrentRange.Source;
        }

        protected virtual IGraphPath GetSubPath()
        {
            return new GraphPath(traces.ToDictionary(), CurrentRange.Target);
        }

        protected virtual void DropState()
        {
            visited.Clear();
            traces.Clear();
        }

        protected virtual IReadOnlyCollection<IVertex> GetUnvisitedNeighbours(IVertex vertex)
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

        private static IGraphPath CreatePath(IReadOnlyList<IGraphPath> subPaths)
        {
            return subPaths.Count switch
            {
                1 => subPaths[0],
                > 1 => new CompositeGraphPath(subPaths),
                _ => NullGraphPath.Instance
            };
        }
    }
}