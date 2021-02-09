using Algorithm.EventArguments;
using Algorithm.Handlers;
using Algorithm.Interface;
using GraphLib.Extensions;
using GraphLib.Infrastructure;
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
            visitedVerticesCoordinates = new List<ICoordinate>();
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
            visitedVerticesCoordinates.Clear();
            parentVertices.Clear();
            accumulatedCosts.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
            if (!Graph.Contains(start, end))
            {
                throw new Exception("Vertices doesn't belong to graph");
            }
            Start = start; End = end;
            RaiseOnAlgorithmStartedEvent(new AlgorithmEventArgs());
            CurrentVertex = Start;
            visitedVerticesCoordinates.Add(Start.Position);
        }

        protected IEnumerable<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(neighbour => !visitedVerticesCoordinates.Contains(neighbour.Position));
        }

        protected virtual void CompletePathfinding()
        {
            RaiseOnAlgorithmFinishedEvent(new AlgorithmEventArgs(visitedVerticesCoordinates.Count));
        }

        protected AlgorithmEventArgs CreateEventArgs(IVertex vertex)
        {
            int visitedCount = visitedVerticesCoordinates.Count;
            bool isExtreme = vertex.IsEqual(Start) 
                || vertex.IsEqual(End) || vertex.IsDefault;
            return new AlgorithmEventArgs(visitedCount, isExtreme, vertex);
        }

        protected List<ICoordinate> visitedVerticesCoordinates;
        protected Dictionary<ICoordinate, IVertex> parentVertices;
        protected Dictionary<ICoordinate, double> accumulatedCosts;
    }
}
