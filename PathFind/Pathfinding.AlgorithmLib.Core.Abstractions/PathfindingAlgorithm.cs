using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Core.NullObjects;
using Shared.Extensions;
using Shared.Primitives;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal record IsExternalInit;
}

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    using Traces = Dictionary<ICoordinate, IVertex>;

    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected sealed record SubRange(IVertex Source, IVertex Target);

        protected readonly IEnumerable<IVertex> pathfindingRange;
        protected readonly HashSet<IVertex> visited = new(new VertexEqualityComparer());
        protected readonly Traces traces = new(new CoordinateEqualityComparer());
        protected readonly TStorage storage = new();

        protected SubRange CurrentRange { get; set; }

        protected IVertex CurrentVertex { get; set; } = NullVertex.Instance;

        private bool IsDestination => CurrentVertex.Equals(CurrentRange.Target);

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
                    while (!IsDestination)
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

        protected abstract IVertex GetNextVertex();

        protected abstract void InspectVertex(IVertex vertex);

        protected abstract void VisitCurrentVertex();

        protected virtual void PrepareForSubPathfinding(SubRange range)
        {
            CurrentRange = range;
            CurrentVertex = CurrentRange.Source;
        }

        protected virtual IGraphPath GetSubPath()
        {
            var traces = this.traces.ToDictionary();
            return new GraphPath(traces, CurrentRange.Target);
        }

        protected virtual void Enqueued(IVertex vertex)
        {
            RaiseVertexEnqueued(vertex);
        }

        protected override void DropState()
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

        private IEnumerable<SubRange> GetSubRanges()
        {
            using (var iterator = pathfindingRange.GetEnumerator())
            {
                if (iterator.MoveNext())
                {
                    var previous = iterator.Current;
                    while (iterator.MoveNext())
                    {
                        var current = iterator.Current;
                        yield return new(previous, current);
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