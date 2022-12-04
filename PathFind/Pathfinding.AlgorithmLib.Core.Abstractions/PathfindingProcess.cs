using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.History.Interface;
using Shared.Process.EventArguments;
using Shared.Process.EventHandlers;
using Shared.Process.Interface;
using System;
using System.Threading;

namespace Pathfinding.AlgorithmLib.Core.Abstractions
{
    public abstract class PathfindingProcess : IAlgorithm<IGraphPath>, IHistoryPageKey, IProcess, IPausable, IInterruptable, IDisposable
    {
        private sealed class NullProcess : PathfindingProcess
        {
            public override Guid Id => Guid.Empty;

            public override IGraphPath FindPath() => NullGraphPath.Instance;

            public override void Interrupt() { }

            public override void Pause() { }

            public override void Resume() { }

            protected override void DropState() { }
        }

        public static readonly PathfindingProcess Null = new NullProcess();

        public event PathfindingEventHandler VertexVisited;
        public event PathfindingEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;
        public event ProcessEventHandler Paused;
        public event ProcessEventHandler Resumed;

        private readonly EventWaitHandle pauseEvent;

        public virtual Guid Id { get; }

        public bool IsInProcess { get; private set; } = false;

        public bool IsPaused { get; private set; } = false;

        private bool IsInterrupted { get; set; } = false;

        private bool IsAlgorithmDisposed { get; set; } = false;

        protected PathfindingProcess()
        {
            pauseEvent = new AutoResetEvent(true);
            Id = Guid.NewGuid();
        }

        public abstract IGraphPath FindPath();

        public virtual void Dispose()
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

        public virtual void Interrupt()
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

        public virtual void Pause()
        {
            if (!IsPaused && IsInProcess)
            {
                IsPaused = true;
                Paused?.Invoke(this, new ProcessEventArgs());
            }
        }

        public virtual void Resume()
        {
            if (IsPaused && IsInProcess)
            {
                IsPaused = false;
                pauseEvent.Set();
                Resumed?.Invoke(this, new ProcessEventArgs());
            }
        }

        protected abstract void DropState();

        protected virtual void WaitUntilResumed()
        {
            if (IsPaused && IsInProcess)
            {
                pauseEvent.WaitOne();
            }
        }

        protected void RaiseVertexVisited(PathfindingEventArgs e)
        {
            VertexVisited?.Invoke(this, e);
        }

        protected void RaiseVertexEnqueued(PathfindingEventArgs e)
        {
            VertexEnqueued?.Invoke(this, e);
        }

        protected virtual void PrepareForPathfinding()
        {
            if (IsAlgorithmDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
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
    }
}