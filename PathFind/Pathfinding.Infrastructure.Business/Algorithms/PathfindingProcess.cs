using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Algorithms;
using Pathfinding.Shared.EventHandlers;
using System;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class PathfindingProcess : IAlgorithm<IGraphPath>, IDisposable
    {
        private sealed class NullProcess : PathfindingProcess
        {
            public override IGraphPath FindPath() => NullGraphPath.Instance;
        }

        public static readonly PathfindingProcess Idle = new NullProcess();

        public event VerticesEnqueuedEventHandler VertexEnqueued;
        public event PathfindingEventHandler VertexVisited;
        public event SubPathFoundEventHandler SubPathFound;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;

        private bool IsAlgorithmDisposed { get; set; } = false;

        protected PathfindingProcess()
        {
            
        }

        public abstract IGraphPath FindPath();

        public virtual void Dispose()
        {
            IsAlgorithmDisposed = true;
            Started = null;
            Finished = null;
            VertexEnqueued = null;
            VertexVisited = null;
            SubPathFound = null;
        }

        protected void RaiseVertexVisited(IVertex vertex)
        {
            VertexVisited?.Invoke(this, new(vertex));
        }

        protected void RaiseVertexEnqueued(IVertex vertex,
            IEnumerable<IVertex> vertices)
        {
            VertexEnqueued?.Invoke(this, new(vertex, vertices));
        }

        protected void RaiseSubPathFound(IGraphPath subPath)
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

        protected virtual void PrepareForPathfinding()
        {
            Started?.Invoke(this, new());
        }

        protected virtual void CompletePathfinding()
        {
            Finished?.Invoke(this, new());
        }
    }
}