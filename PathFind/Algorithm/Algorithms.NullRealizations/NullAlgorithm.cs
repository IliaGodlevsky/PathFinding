using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using NullObject.Attributes;

namespace Algorithm.NullRealizations
{
    /// <summary>
    /// Represents a null analog for 
    /// <see cref="IAlgorithm"/> interface
    /// </summary>
    [Null]
    public sealed class NullAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHandler Started;
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler Finished;
        public event AlgorithmEventHandler VertexEnqueued;
        public event InterruptEventHanlder Interrupted;

        public NullAlgorithm()
        {

        }

        public IGraphPath FindPath()
        {
            Started?.Invoke(this, AlgorithmEventArgs.Empty);
            VertexVisited?.Invoke(this, AlgorithmEventArgs.Empty);
            VertexEnqueued?.Invoke(this, AlgorithmEventArgs.Empty);
            Finished?.Invoke(this, AlgorithmEventArgs.Empty);
            return new NullGraphPath();
        }

        public void Interrupt()
        {
            Interrupted?.Invoke(this, new InterruptEventArgs());
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
