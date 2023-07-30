using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Core.NullObjects;
using Shared.Extensions;
using Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    using Traces = Dictionary<ICoordinate, IVertex>;

    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected readonly IEnumerable<IVertex> pathfindingRange;
        protected readonly HashSet<IVertex> visited = new(VertexEqualityComparer.Interface);
        protected readonly Traces traces = new(CoordinateEqualityComparer.Interface);
        protected readonly TStorage storage = new();

        protected (IVertex Source, IVertex Target) CurrentRange { get; set; }

        protected IVertex CurrentVertex { get; set; } = NullVertex.Instance;

        protected PathfindingAlgorithm(IEnumerable<IVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
        }

        public sealed override IGraphPath FindPath()
        {
            ThrowIfDisposed();
            var subPaths = new List<IGraphPath>();
            using (Disposable.Use(CompletePathfinding))
            {
                PrepareForPathfinding();
                foreach (var range in GetSubRanges())
                {
                    PrepareForSubPathfinding(range);
                    while (!IsDestination())
                    {
                        ThrowIfInterrupted();
                        WaitUntilResumed();
                        InspectVertex(CurrentVertex);
                        CurrentVertex = GetNextVertex();
                        VisitCurrentVertex();
                    }
                    subPaths.Add(GetSubPath());
                    DropState();
                }
            }
            return CreatePath(subPaths);
        }

        public override void Dispose()
        {
            base.Dispose();
            DropState();
        }

        protected abstract IVertex GetNextVertex();

        protected abstract void InspectVertex(IVertex vertex);

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

        protected virtual void Enqueued(IVertex vertex)
        {
            RaiseVertexEnqueued(vertex);
        }

        protected virtual void DropState()
        {
            visited.Clear();
            traces.Clear();
        }

        protected IReadOnlyCollection<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(v => !v.IsObstacle && !visited.Contains(v))
                .ToArray();
        }

        private IEnumerable<(IVertex Source, IVertex Target)> GetSubRanges()
        {
            using (var iterator = pathfindingRange.GetEnumerator())
            {
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