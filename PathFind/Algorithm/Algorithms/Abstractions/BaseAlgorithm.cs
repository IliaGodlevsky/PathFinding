using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.Algorithms.Abstractions
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public event Action OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event Action OnFinished;
        public event Action<IVertex> OnVertexEnqueued;

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
            OnStarted?.Invoke();
        }

        protected void RaiseOnAlgorithmFinishedEvent()
        {
            OnFinished?.Invoke();
        }

        protected void RaiseOnVertexVisitedEvent()
        {
            OnVertexVisited?.Invoke(CurrentVertex);
        }

        protected void RaiseOnVertexEnqueuedEvent(IVertex vertex)
        {
            OnVertexEnqueued?.Invoke(vertex);
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
