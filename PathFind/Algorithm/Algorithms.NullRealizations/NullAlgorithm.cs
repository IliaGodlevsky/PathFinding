using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using NullObject.Attributes;
using System;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// Represents a null analog for 
    /// <see cref="IAlgorithm"/> interface
    /// </summary>
    [Null]
    public sealed class NullAlgorithm : IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;

        public NullAlgorithm()
        {

        }

        public IGraphPath FindPath()
        {
            Started?.Invoke(this, new ProcessEventArgs());
            VertexVisited?.Invoke(this, AlgorithmEventArgs.Empty);
            VertexEnqueued?.Invoke(this, AlgorithmEventArgs.Empty);
            Finished?.Invoke(this, new ProcessEventArgs());
            return new NullGraphPath();
        }

        public void Interrupt()
        {
            Interrupted?.Invoke(this, new ProcessEventArgs());
        }

        public void Dispose()
        {
            Started = null;
            VertexVisited = null;
            VertexEnqueued = null;
            Finished = null;
            Interrupted = null;
        }
    }
}
