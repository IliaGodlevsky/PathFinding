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

namespace Algorithm.Base
{
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public static IAlgorithm Default => new DefaultAlgorithm();

        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;

        public virtual IGraph Graph { get; set; }

        public BaseAlgorithm() : this(BaseGraph.NullGraph)
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
            Graph = BaseGraph.NullGraph;
            OnStarted = null;
            OnFinished = null;
            OnVertexEnqueued = null;
            OnVertexVisited = null;
            visitedVertices.Clear();
            parentVertices.Clear();
            accumulatedCosts.Clear();
        }

        public virtual async Task<IGraphPath> FindPathAsync(IEndPoints endpoints)
        {
            return await Task.Run(() => FindPath(endpoints));
        }

        public abstract IGraphPath FindPath(IEndPoints endPoints);

        protected IVertex CurrentVertex { get; set; }

        protected abstract IVertex NextVertex { get; }

        protected virtual bool IsDestination()
        {
            return CurrentVertex.IsEqual(endPoints.End)
                || CurrentVertex.IsDefault();
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

        protected virtual void PrepareForPathfinding(IEndPoints endpoints)
        {
            if (Graph.Contains(endpoints))
            {
                endPoints = endpoints;
                var args = new AlgorithmEventArgs();
                RaiseOnAlgorithmStartedEvent(args);
                CurrentVertex = endPoints.Start;
                visitedVertices[CurrentVertex.Position] = CurrentVertex;
                return;
            }

            string paramName = nameof(endpoints);
            string graphName = nameof(Graph);
            string message = $"{paramName} don't belong to {graphName}";
            throw new ArgumentException(message);
        }

        protected virtual void CompletePathfinding()
        {
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
    }
}
