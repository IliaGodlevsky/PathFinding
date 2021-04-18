using Algorithm.Common;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Base
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public static IAlgorithm Default => new DefaultAlgorithm();

        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;
        public event EventHandler OnInterrupted;

        private BaseAlgorithm()
        {
            graph = new NullGraph();
            visitedVertices = new Dictionary<ICoordinate, IVertex>();
            parentVertices = new Dictionary<ICoordinate, IVertex>();
            accumulatedCosts = new Dictionary<ICoordinate, double>();
        }

        protected BaseAlgorithm(IGraph graph) : this()
        {
            this.graph = graph;

        }

        /// <summary>
        /// Stops path finding
        /// </summary>
        public virtual void Interrupt()
        {
            isInterruptRequested = true;
            var args = CreateEventArgs(CurrentVertex);
            OnInterrupted?.Invoke(this, args);
        }

        protected virtual void Reset()
        {
            OnStarted = null;
            OnFinished = null;
            OnVertexEnqueued = null;
            OnVertexVisited = null;
            OnInterrupted = null;
            visitedVertices.Clear();
            parentVertices.Clear();
            accumulatedCosts.Clear();
            isInterruptRequested = false;
        }

        /// <summary>
        /// Finds the shortest path between <paramref name="endPoints"/>
        /// </summary>
        /// <param name="endPoints"></param>
        /// <param name="graph"></param>
        /// <returns>An array of vertices, that represent the shortest path</returns>
        public abstract IGraphPath FindPath(IEndPoints endPoints);

        protected IVertex CurrentVertex { get; set; }

        protected abstract IVertex NextVertex { get; }

        protected virtual bool IsDestination()
        {
            return CurrentVertex.IsEqual(endPoints.End)
                || CurrentVertex.IsDefault()
                || isInterruptRequested;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoints"></param>
        /// <exception cref="ArgumentException"></exception>
        protected virtual void PrepareForPathfinding(IEndPoints endpoints)
        {
            if (graph.Contains(endpoints))
            {
                endPoints = endpoints;
                CurrentVertex = endPoints.Start;
                visitedVertices[CurrentVertex.Position] = CurrentVertex;
                var args = CreateEventArgs(CurrentVertex);
                RaiseOnAlgorithmStartedEvent(args);
                return;
            }

            string paramName = nameof(endpoints);
            string graphName = nameof(this.graph);
            string message = $"{paramName} don't belong to {graphName}";
            throw new ArgumentException(message);
        }

        protected virtual void CompletePathfinding()
        {
            isInterruptRequested = false;
            var visitedCount = visitedVertices.Count(IsNotDefault);
            var args = new AlgorithmEventArgs(visitedCount);
            RaiseOnAlgorithmFinishedEvent(args);
        }

        protected AlgorithmEventArgs CreateEventArgs(IVertex vertex)
        {
            int visitedCount = visitedVertices.Count;
            bool isEndPoint = endPoints.IsEndPoint(vertex);
            return new AlgorithmEventArgs(visitedCount, isEndPoint, vertex);
        }

        protected bool IsNotDefault(KeyValuePair<ICoordinate, IVertex> vertex)
        {
            return !vertex.Value.IsDefault();
        }

        protected IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(VertexIsNotVisited)
                .Where(VertexIsNotObstacle);
        }

        protected bool VertexIsNotVisited(IVertex vertex)
        {
            return !visitedVertices.TryGetValue(vertex.Position, out _);
        }

        protected bool VertexIsNotObstacle(IVertex vertex)
        {
            return !vertex.IsObstacle;
        }

        protected ICoordinate GetPosition(IVertex vertex)
        {
            return vertex.Position;
        }

        protected virtual double GetAccumulatedCost(IVertex vertex)
        {
            return accumulatedCosts[vertex.Position];
        }

        public void Dispose()
        {
            Reset();
        }

        protected Dictionary<ICoordinate, IVertex> visitedVertices;
        protected Dictionary<ICoordinate, IVertex> parentVertices;
        protected Dictionary<ICoordinate, double> accumulatedCosts;

        protected IGraph graph;
        protected IEndPoints endPoints;

        private bool isInterruptRequested;
    }
}
