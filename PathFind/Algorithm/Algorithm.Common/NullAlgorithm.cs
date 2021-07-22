using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using AssembleClassesLib.Attributes;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using NullObject.Attributes;

namespace Algorithm.Common
{
    /// <summary>
    /// Represents a null analog for 
    /// <see cref="IAlgorithm"/> interface
    /// </summary>
    [Null]
    [NotLoadable]
    public sealed class NullAlgorithm : IAlgorithm
    {
        public bool IsInterruptRequested => true;

        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;
        public event InterruptEventHanlder OnInterrupted;

        public NullAlgorithm()
        {

        }

        public IGraphPath FindPath()
        {
            OnStarted?.Invoke(this, AlgorithmEventArgs.Empty);
            OnVertexVisited?.Invoke(this, AlgorithmEventArgs.Empty);
            OnVertexEnqueued?.Invoke(this, AlgorithmEventArgs.Empty);
            OnFinished?.Invoke(this, AlgorithmEventArgs.Empty);
            return new NullGraphPath();
        }

        public void Interrupt()
        {
            OnInterrupted?.Invoke(this, new InterruptEventArgs());
        }

        public void Dispose()
        {
            OnStarted = null;
            OnVertexVisited = null;
            OnVertexEnqueued = null;
            OnFinished = null;
            OnInterrupted = null;
        }
    }
}
