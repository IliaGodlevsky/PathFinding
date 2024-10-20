using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Algorithms;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class PathfindingProcess : IAlgorithm<IGraphPath>, IDisposable
    {
        public event VertexProcessedEventHandler VertexProcessed;
        public event SubPathFoundEventHandler SubPathFound;

        private bool IsAlgorithmDisposed { get; set; } = false;

        public abstract IGraphPath FindPath();

        public virtual void Dispose()
        {
            IsAlgorithmDisposed = true;
            VertexProcessed = null;
            SubPathFound = null;
        }

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
    }
}