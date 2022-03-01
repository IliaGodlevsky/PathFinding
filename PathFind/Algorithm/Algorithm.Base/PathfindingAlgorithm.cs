using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using Common;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using NullObject.Extensions;
using System;
using System.Threading;

namespace Algorithm.Base
{
    public abstract class PathfindingAlgorithm : IAlgorithm, IProcess, IPausable, IInterruptable, IDisposable
    {
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;
        public event ProcessEventHandler Paused;
        public event ProcessEventHandler Resumed;

        public bool IsInProcess { get; private set; }
        public bool IsPaused { get; private set; }
        private bool IsInterruptRequested { get; set; }
        protected IVertex CurrentVertex { get; set; }
        protected abstract IVertex NextVertex { get; }
        protected bool IsTerminatedPrematurely => !CurrentVertex.IsNull() && !IsInterruptRequested;

        public abstract IGraphPath FindPath();

        public void Interrupt()
        {
            pauseEvent.Set();
            IsPaused = false;
            IsInterruptRequested = true;
            IsInProcess = false;
            Interrupted?.Invoke(this, new ProcessEventArgs());
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

        protected virtual void WaitResumed()
        {
            if (IsPaused)
            {
                pauseEvent.WaitOne();
            }
        }

        protected PathfindingAlgorithm(IEndPoints endPoints)
        {
            visitedVertices = new VisitedVertices();
            parentVertices = new ParentVertices();
            this.endPoints = endPoints;
            pauseEvent = new AutoResetEvent(true);
        }

        protected virtual void Reset()
        {
            visitedVertices.Clear();
            parentVertices.Clear();
            IsInterruptRequested = false;
            IsPaused = false;
        }

        protected virtual bool IsDestination(IEndPoints endPoints)
        {
            return endPoints.Target.IsEqual(CurrentVertex) || !IsTerminatedPrematurely;
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
            Reset();
            IsInProcess = true;
            Started?.Invoke(this, new ProcessEventArgs());
        }

        protected virtual void CompletePathfinding()
        {
            IsInProcess = false;
            Finished?.Invoke(this, new ProcessEventArgs());
        }

        public void Dispose()
        {           
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

        protected readonly IVisitedVertices visitedVertices;
        protected readonly IParentVertices parentVertices;
        protected readonly IEndPoints endPoints;
        private readonly AutoResetEvent pauseEvent;
    }
}