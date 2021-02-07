using Algorithm.EventArguments;
using Algorithm.Handlers;
using Algorithm.Interface;
using GraphLib.Interface;
using GraphLib.NullObjects;

namespace Algorithm.Base
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;

        public virtual IGraph Graph { get; set; }

        public bool IsDefault => false;

        public BaseAlgorithm() : this(new NullGraph())
        {

        }

        public BaseAlgorithm(IGraph graph)
        {
            Graph = graph;
        }

        public virtual void Reset()
        {
            Graph = new NullGraph();
            OnStarted = null;
            OnFinished = null;
            OnVertexEnqueued = null;
            OnVertexVisited = null;
        }

        public abstract void FindPath();

        protected IVertex CurrentVertex { get; set; }

        protected abstract IVertex NextVertex { get; }

        protected virtual bool IsDestination()
        {
            return CurrentVertex.IsEnd && ReferenceEquals(CurrentVertex, Graph.End);
        }

        protected void RaiseOnAlgorithmStartedEvent(AlgorithmEventArgs e)
        {
            OnStarted?.Invoke(this, e);
        }

        protected void RaiseOnAlgorithmFinishedEvent(AlgorithmEventArgs e)
        {
            OnFinished?.Invoke(this, e);
        }

        protected void RaiseOnVertexVisitedEvent(AlgorithmEventArgs e)
        {
            OnVertexVisited?.Invoke(this, e);
        }

        protected void RaiseOnVertexEnqueuedEvent(AlgorithmEventArgs e)
        {
            OnVertexEnqueued?.Invoke(this, e);
        }

        protected virtual void PrepareForPathfinding()
        {
            RaiseOnAlgorithmStartedEvent(new AlgorithmEventArgs());
            CurrentVertex = Graph.Start;
            CurrentVertex.IsVisited = true;
        }

        protected virtual void CompletePathfinding()
        {
            RaiseOnAlgorithmFinishedEvent(new AlgorithmEventArgs(Graph));
        }
    }
}
