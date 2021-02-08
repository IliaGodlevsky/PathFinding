using Algorithm.EventArguments;
using Algorithm.Handlers;
using Algorithm.Interface;
using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System.Collections.Generic;
using System.Linq;

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
            visitedVertices = new List<ICoordinate>();
            parentVertices = new Dictionary<ICoordinate, IVertex>();
            accumulatedCosts = new Dictionary<ICoordinate, double>();
        }

        public virtual void Reset()
        {
            Graph = new NullGraph();
            OnStarted = null;
            OnFinished = null;
            OnVertexEnqueued = null;
            OnVertexVisited = null;
            visitedVertices.Clear();
            parentVertices.Clear();
            accumulatedCosts.Clear();
        }

        public abstract GraphPath FindPath(IVertex start, IVertex end);

        protected IVertex CurrentVertex { get; set; }

        protected IVertex Start { get; set; }

        protected IVertex End { get; set; }

        protected abstract IVertex NextVertex { get; }

        protected virtual bool IsDestination()
        {
            return CurrentVertex.IsEqual(End) || CurrentVertex.IsDefault;
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

        protected virtual void PrepareForPathfinding(IVertex start, IVertex end)
        {
            Start = start; End = end;
            RaiseOnAlgorithmStartedEvent(new AlgorithmEventArgs());
            CurrentVertex = Start;
            visitedVertices.Add(Start.Position);
        }

        protected IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(neighbour => !visitedVertices.Contains(neighbour.Position));
        }

        protected virtual void CompletePathfinding()
        {
            RaiseOnAlgorithmFinishedEvent(new AlgorithmEventArgs(visitedVertices.Count));
        }

        protected List<ICoordinate> visitedVertices;
        protected Dictionary<ICoordinate, IVertex> parentVertices;
        protected Dictionary<ICoordinate, double> accumulatedCosts;
    }
}
