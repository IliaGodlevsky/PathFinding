using Algorithm.Delegates;
using Algorithm.EventArguments;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.Algorithms.Abstractions
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
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

        protected void RaiseOnAlgorithmStartedEvent(AlgorithmEventArgs e)
        {
            OnStarted?.Invoke(this, e);
        }

        protected void RaiseOnAlgorithmFinishedEvent(AlgorithmEventArgs e)
        {
            OnFinished?.Invoke(this, e);
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
            var args = new AlgorithmEventArgs(Graph);
            RaiseOnAlgorithmStartedEvent(args);
            CurrentVertex = Graph.Start;
            CurrentVertex.IsVisited = true;
        }

        protected virtual void CompletePathfinding()
        {
            var args = new AlgorithmEventArgs(Graph);
            RaiseOnAlgorithmFinishedEvent(args);
        }
    }
}
