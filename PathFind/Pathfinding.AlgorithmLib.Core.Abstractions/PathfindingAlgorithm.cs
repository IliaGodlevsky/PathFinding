using Pathfinding.AlgorithmLib.Core.Events;
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

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal record IsExternalInit;
}

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    using Traces = Dictionary<ICoordinate, IVertex>;

    internal abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected record SubRange(IVertex Source, IVertex Target);

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
            var paths = new List<IGraphPath>();
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
                    paths.Add(GetSubPath());
                    DropState();
                }
            }
            return GenerateResult(paths);
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
            var traces = this.traces.ToDictionary().ToReadOnly();
            return new GraphPath(traces, CurrentRange.Target);
        }

        protected virtual void Enqueued(IVertex vertex)
        {
            RaiseVertexEnqueued(new PathfindingEventArgs(vertex));
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
                .ToReadOnly();
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

        private static IGraphPath GenerateResult(IReadOnlyCollection<IGraphPath> paths)
        {
            return paths.Count switch
            {
                1 => paths.First(),
                > 1 => new CompositeGraphPath(paths),
                _ => NullGraphPath.Interface
            };
        }
    }
}