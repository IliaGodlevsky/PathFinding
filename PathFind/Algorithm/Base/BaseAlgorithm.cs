using Algorithm.EventArguments;
using Algorithm.Handlers;
using Algorithm.Interface;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
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
            visitedVertices = new Dictionary<ICoordinate, IVertex>();
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

        public abstract IGraphPath FindPath(IEndPoints endPoints);

        protected IVertex CurrentVertex { get; set; }

        protected abstract IVertex NextVertex { get; }

        protected virtual bool IsDestination()
        {
            return CurrentVertex.IsEqual(endPoints.End) || CurrentVertex.IsDefault;
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

        protected virtual void PrepareForPathfinding(IEndPoints endPoints)
        {
            if (Graph.Contains(endPoints))
            {
                this.endPoints = endPoints;
                var args = new AlgorithmEventArgs();
                RaiseOnAlgorithmStartedEvent(args);
                CurrentVertex = this.endPoints.Start;
                visitedVertices[CurrentVertex.Position] = CurrentVertex;
            }
            else
            {
                throw new Exception("Endpoints doesn't belong to graph");
            }
        }

        protected virtual void CompletePathfinding()
        {
            var visitedCount = visitedVertices
                .Count(item => !item.Value.IsDefault);
            var args = new AlgorithmEventArgs(visitedCount);
            RaiseOnAlgorithmFinishedEvent(args);
        }

        protected AlgorithmEventArgs CreateEventArgs(IVertex vertex)
        {
            int visitedCount = visitedVertices.Count;
            bool isEndPoint = endPoints.IsEndPoint(vertex);
            return new AlgorithmEventArgs(visitedCount, isEndPoint, vertex);
        }

        protected IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours.Where(IsNotVisited);
        }

        protected bool IsNotVisited(IVertex vertex)
        {
            return !visitedVertices.TryGetValue(vertex.Position, out _);
        }

        protected ICoordinate GetPosition(IVertex vertex)
        {
            return vertex.Position;
        }

        protected double GetAccumulatedCost(IVertex vertex)
        {
            return accumulatedCosts[vertex.Position];
        }

        protected Dictionary<ICoordinate, IVertex> visitedVertices;
        protected Dictionary<ICoordinate, IVertex> parentVertices;
        protected Dictionary<ICoordinate, double> accumulatedCosts;
        protected IEndPoints endPoints;
    }
}
