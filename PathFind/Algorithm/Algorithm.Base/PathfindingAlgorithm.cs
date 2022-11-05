using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Algorithm.Realizations.GraphPaths;
using Common.Disposables;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.Utility;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal record IsExternalInit;
}

namespace Algorithm.Base
{
    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : IEnumerable<IVertex>, new()
    {
        protected record Range(IVertex Source, IVertex Target);

        protected readonly ICollection<IVertex> visited;
        protected readonly IDictionary<ICoordinate, IVertex> traces;
        protected readonly IEndPoints endPoints;
        protected readonly TStorage storage;

        protected Range CurrentRange { get; set; }

        protected IVertex CurrentVertex { get; set; } = NullVertex.Instance;

        private bool IsDestination => CurrentVertex.Equals(CurrentRange.Target);

        protected PathfindingAlgorithm(IEndPoints endPoints)
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
            RaiseVertexEnqueued(new AlgorithmEventArgs(vertex));
        }

        protected override void DropState()
        {
            visited.Clear();
            traces.Clear();
            base.DropState();
        }

        protected IReadOnlyCollection<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(vertex => !vertex.IsObstacle && !visited.Contains(vertex))
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