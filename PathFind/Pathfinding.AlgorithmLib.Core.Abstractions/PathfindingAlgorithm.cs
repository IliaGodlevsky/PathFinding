using Algorithm.Realizations.GraphPaths;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Comparers;
using Pathfinding.GraphLib.Core.NullObjects;
using Shared.Extensions;
using Shared.Primitives;
using Shared.Primitives.Single;
using System;
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
    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected record Range(IVertex Source, IVertex Target);

        protected readonly ICollection<IVertex> visited;
        protected readonly IDictionary<ICoordinate, IVertex> traces;
        protected readonly IPathfindingRange endPoints;
        protected readonly TStorage storage;

        protected Range CurrentRange { get; set; }

        protected IVertex CurrentVertex { get; set; } = NullVertex.Instance;

        private bool IsDestination => CurrentVertex.Equals(CurrentRange.Target);

        protected PathfindingAlgorithm(IPathfindingRange endPoints)
        {
            storage = new TStorage();
            this.endPoints = endPoints;
            visited = new HashSet<IVertex>(new VertexEqualityComparer());
            traces = new Dictionary<ICoordinate, IVertex>(new CoordinateEqualityComparer());
        }

        public sealed override IGraphPath FindPath()
        {
            PrepareForPathfinding();
            using (Disposable.Use(CompletePathfinding))
            {
                var path = NullGraphPath.Interface;
                foreach (var endPoint in GetSubEndPoints())
                {
                    PrepareForSubPathfinding(endPoint);
                    while (!IsDestination)
                    {
                        ThrowIfInterrupted();
                        WaitUntilResumed();
                        InspectVertex(CurrentVertex);
                        CurrentVertex = GetNextVertex();
                        VisitCurrentVertex();
                    }
                    var subPath = CreateGraphPath();
                    path = new CompositeGraphPath(path, subPath);
                    DropState();
                }
                return path;
            }
        }

        protected abstract IVertex GetNextVertex();

        protected abstract void InspectVertex(IVertex vertex);

        protected abstract void VisitCurrentVertex();

        protected virtual void PrepareForSubPathfinding(Range range)
        {
            CurrentRange = range;
            CurrentVertex = CurrentRange.Source;
        }

        protected virtual IGraphPath CreateGraphPath()
        {
            return new GraphPath(traces.ToReadOnly(), CurrentRange.Target);
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

        private IEnumerable<Range> GetSubEndPoints()
        {
            using (var iterator = endPoints.GetEnumerator())
            {
                iterator.MoveNext();
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
}