using Algorithm.EventArguments;
using Algorithm.Handlers;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;

namespace Algorithm.Algorithms.Abstractions
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;

        public IGraph Graph { get; protected set; }

        public bool IsDefault => false;

        public BaseAlgorithm(IGraph graph)
        {
            Graph = graph;
        }

        public abstract void FindPath();

        protected IVertex CurrentVertex { get; set; }

        protected abstract IVertex NextVertex { get; }

        protected virtual bool IsDestination => CurrentVertex.IsEnd;

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
