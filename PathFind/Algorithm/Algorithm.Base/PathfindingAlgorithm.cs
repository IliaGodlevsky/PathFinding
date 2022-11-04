using Algorithm.Exceptions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using System;
using System.Threading;

namespace Algorithm.Base
{
    public abstract class PathfindingAlgorithm : IAlgorithm<IGraphPath>, IProcess, IPausable, IInterruptable, IDisposable
    {
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;
        public event ProcessEventHandler Paused;
        public event ProcessEventHandler Resumed;

        private readonly EventWaitHandle pauseEvent;

        public bool IsInProcess { get; private set; } = false;

        public bool IsPaused { get; private set; } = false;

        private bool IsInterrupted { get; set; } = false;

        private bool IsAlgorithmDisposed { get; set; } = false;

        protected PathfindingAlgorithm()
        {
            pauseEvent = new AutoResetEvent(true);
        }

        public abstract IGraphPath FindPath();

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
            DropState();
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
            if (!IsPaused && IsInProcess)
            {
                IsPaused = true;
                Paused?.Invoke(this, new ProcessEventArgs());
            }
        }

        public void Resume()
        {
            if (IsPaused && IsInProcess)
            {
                IsPaused = false;
                pauseEvent.Set();
                Resumed?.Invoke(this, new ProcessEventArgs());
            }
        }

        protected virtual void WaitUntilResumed()
        {
            if (IsPaused && IsInProcess)
            {
                pauseEvent.WaitOne();
            }
        }

        protected virtual void DropState()
        {
            IsPaused = false;           
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
            if (!IsAlgorithmDisposed)
            {
                DropState();
                IsInProcess = true;
                Started?.Invoke(this, new ProcessEventArgs());
                return;
            }
            throw new ObjectDisposedException(GetType().Name);
        }

        protected void ThrowIfInterrupted()
        {
            if (IsInterrupted && IsInProcess)
            {
                throw new AlgorithmInterruptedException(this);
            }
        }

        protected virtual void CompletePathfinding()
        {
            IsInProcess = false;
            Finished?.Invoke(this, new ProcessEventArgs());
        }
    }
}