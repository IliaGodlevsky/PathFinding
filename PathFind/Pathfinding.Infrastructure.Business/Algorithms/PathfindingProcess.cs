using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Algorithms;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class PathfindingProcess : IAlgorithm<IGraphPath>, IDisposable
    {
        protected readonly IEnumerable<IVertex> pathfindingRange;

        public event VertexProcessedEventHandler VertexProcessed;
        public event SubPathFoundEventHandler SubPathFound;

        private bool IsAlgorithmDisposed { get; set; } = false;

        protected PathfindingProcess(IEnumerable<IVertex> pathfindingRange)
        {
            this.pathfindingRange = pathfindingRange;
        }

        public virtual IGraphPath FindPath()
        {
            ThrowIfDisposed();
            var subPaths = new List<IGraphPath>();
            foreach (var range in GetSubRanges())
            {
                PrepareForSubPathfinding(range);
                while (!IsDestination())
                {
                    InspectCurrentVertex();
                    MoveNextVertex();
                    VisitCurrentVertex();
                }
                var subPath = GetSubPath();
                subPaths.Add(subPath);
                RaiseSubPathFound(subPath);
                DropState();
            }
            return CreatePath(subPaths);
        }

        public virtual void Dispose()
        {
            IsAlgorithmDisposed = true;
            VertexProcessed = null;
            SubPathFound = null;
        }

        protected abstract void MoveNextVertex();

        protected abstract void InspectCurrentVertex();

        protected abstract void VisitCurrentVertex();

        protected abstract bool IsDestination();

        protected abstract void DropState();

        protected abstract void PrepareForSubPathfinding((IVertex Source, IVertex Target) range);

        protected abstract IGraphPath GetSubPath();

        protected void RaiseVertexProcessed(IVertex vertex,
            IEnumerable<IVertex> vertices)
        {
            VertexProcessed?.Invoke(this, new(vertex, vertices));
        }

        protected void RaiseSubPathFound(IReadOnlyCollection<Coordinate> subPath)
        {
            SubPathFound?.Invoke(this, new(subPath));
        }

        protected void ThrowIfDisposed()
        {
            if (IsAlgorithmDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
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