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

        protected void RaiseOnAlgorithmStartedEvent()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs(Graph));
        }

        protected void RaiseOnAlgorithmFinishedEvent()
        {
            OnFinished?.Invoke(this, new AlgorithmEventArgs(Graph));
        }

        protected void RaiseOnVertexVisitedEvent()
        {
            OnVertexVisited?.Invoke(this, new AlgorithmEventArgs(Graph, CurrentVertex));
        }

        protected void RaiseOnVertexEnqueuedEvent(IVertex vertex)
        {
            OnVertexEnqueued?.Invoke(this, new AlgorithmEventArgs(Graph, vertex));
        }

        protected virtual void PrepareForPathfinding()
        {
            RaiseOnAlgorithmStartedEvent();
            CurrentVertex = Graph.Start;
            CurrentVertex.IsVisited = true;
        }

        protected virtual void CompletePathfinding()
        {
            RaiseOnAlgorithmFinishedEvent();
        }
    }
}
