using Algorithm.Exceptions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Algorithm.Base
{
    public abstract class PathfindingAlgorithm : IAlgorithm<IGraphPath>, ICloneable<PathfindingAlgorithm>, 
        IProcess, IPausable, IInterruptable, IDisposable
    {
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;
        public event ProcessEventHandler Paused;
        public event ProcessEventHandler Resumed;

        private readonly EventWaitHandle pauseEvent;
        protected readonly IVisitedVertices visitedVertices;
        protected readonly IParentVertices parentVertices;
        protected readonly IEndPoints endPoints;

        public bool IsInProcess { get; private set; }

        public bool IsPaused { get; private set; }

        protected IVertex CurrentVertex { get; set; }

        private bool IsInterrupted { get; set; } = false;

        private bool IsAlgorithmDisposed { get; set; } = false;

        protected PathfindingAlgorithm(IEndPoints endPoints)
        {
            visitedVertices = new VisitedVertices();
            parentVertices = new ParentVertices();
            this.endPoints = endPoints;
            pauseEvent = new AutoResetEvent(true);
        }

        public abstract IGraphPath FindPath();

        public abstract PathfindingAlgorithm GetClone();

        public void Dispose()
        {
            IsAlgorithmDisposed = true;
            Started = null;
            Finished = null;
            VertexEnqueued = null;
            VertexVisited = null;
            Interrupted = null;
            Paused = null;
            Resumed = null;
            pauseEvent.Dispose();
            Reset();
        }

        public void Interrupt()
        {
            if (IsInProcess)
            {
                pauseEvent.Set();
                IsPaused = false;
                IsInProcess = false;
                IsInterrupted = true;
                Interrupted?.Invoke(this, new ProcessEventArgs());
            }
        }

        public void Pause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Paused?.Invoke(this, new ProcessEventArgs());
            }
        }

        public void Resume()
        {
            if (IsPaused)
            {
                IsPaused = false;
                pauseEvent.Set();
                Resumed?.Invoke(this, new ProcessEventArgs());
            }
        }

        protected virtual void WaitUntilResumed()
        {
            if (IsPaused)
            {
                pauseEvent.WaitOne();
            }
        }

        protected virtual void Reset()
        {
            visitedVertices.Clear();
            parentVertices.Clear();
            IsPaused = false;
        }

        protected virtual bool IsDestination(IEndPoints endPoints)
        {
            return CurrentVertex.Equals(endPoints.Target);
        }

        protected void RaiseVertexVisited(AlgorithmEventArgs e)
        {
            VertexVisited?.Invoke(this, e);
        }

        protected void RaiseVertexEnqueued(AlgorithmEventArgs e)
        {
            VertexEnqueued?.Invoke(this, e);
        }

        protected virtual void PrepareForPathfinding()
        {
            if (IsAlgorithmDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            Reset();
            IsInProcess = true;
            Started?.Invoke(this, new ProcessEventArgs());
        }

        protected void ThrowIfInterrupted()
        {
            if (IsInterrupted)
            {
                throw new AlgorithmInterruptedException(this);
            }
        }

        protected virtual void CompletePathfinding()
        {
            IsInProcess = false;
            Finished?.Invoke(this, new ProcessEventArgs());
        }

        protected IReadOnlyCollection<IVertex> GetUnvisitedVertices(IVertex vertex)
        {
            return vertex
                .Neighbours
                .Where(visitedVertices.IsNotVisited)
                .Where(v => !v.IsObstacle)
                .ToReadOnly();
        }

        protected abstract IVertex GetNextVertex();
    }
}