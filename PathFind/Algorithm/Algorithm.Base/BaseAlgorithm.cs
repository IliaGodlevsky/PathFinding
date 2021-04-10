using Algorithm.Common;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algorithm.Common.Exceptions;

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

        public virtual IGraph Graph { get; set; }

        protected BaseAlgorithm() : this(BaseGraph.NullGraph)
        {

        }

        protected BaseAlgorithm(IGraph graph)
        {
            Graph = graph;
            visitedVertices = new Dictionary<ICoordinate, IVertex>();
            parentVertices = new Dictionary<ICoordinate, IVertex>();
            accumulatedCosts = new Dictionary<ICoordinate, double>();
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

        public virtual void Reset()
        {
            Graph = BaseGraph.NullGraph;
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

        public virtual async Task<IGraphPath> FindPathAsync(IEndPoints endpoints)
        {
            return await Task.Run(() => FindPath(endpoints));
        }

        /// <summary>
        /// Finds the shortest path between <paramref name="endPoints"/>
        /// </summary>
        /// <param name="endPoints"></param>
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
        /// <exception cref="EndPointsDontBelongToGraphException"></exception>
        protected virtual void PrepareForPathfinding(IEndPoints endpoints)
        {
            if (Graph.Contains(endpoints))
            {
                endPoints = endpoints;
                CurrentVertex = endPoints.Start;
                visitedVertices[CurrentVertex.Position] = CurrentVertex;
                var args = CreateEventArgs(CurrentVertex);
                RaiseOnAlgorithmStartedEvent(args);
                return;
            }

            string paramName = nameof(endpoints);
            string graphName = nameof(Graph);
            string message = $"{paramName} don't belong to {graphName}";
            throw new EndPointsDontBelongToGraphException(message);
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

        protected Dictionary<ICoordinate, IVertex> visitedVertices;
        protected Dictionary<ICoordinate, IVertex> parentVertices;
        protected Dictionary<ICoordinate, double> accumulatedCosts;

        protected IEndPoints endPoints;

        private bool isInterruptRequested;
    }
}
