using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Algorithm.Сompanions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using NullObject.Extensions;
using System;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all pathfinding algorithms
    /// </summary>
    public abstract class BaseAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;
        /// <summary>
        /// Occurs when algorithms is requested to be interrupted
        /// </summary>
        public event EventHandler OnInterrupted;

        /// <summary>
        /// Finds path in graph
        /// </summary>
        /// <returns>Graph path for specified graph</returns>
        public abstract IGraphPath FindPath();

        /// <summary>
        /// Interrupts the pathfinding process so the 
        /// algorithms finishes the current iteration and stops
        /// </summary>
        public virtual void Interrupt()
        {
            isInterruptRequested = true;
            var args = CreateEventArgs(CurrentVertex);
            OnInterrupted?.Invoke(this, args);
        }

        protected BaseAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            visitedVertices = new VisitedVertices();
            parentVertices = new ParentVertices();
            this.graph = graph;
            this.endPoints = new EndPoints(endPoints);
        }

        /// <summary>
        /// Clears all algorithm's pathfinding history and events
        /// </summary>
        protected virtual void Reset()
        {
            OnStarted = null;
            OnFinished = null;
            OnVertexEnqueued = null;
            OnVertexVisited = null;
            OnInterrupted = null;
            visitedVertices.Clear();
            parentVertices.Clear();
            isInterruptRequested = false;
        }

        /// <summary>
        /// The vertex, that the algorithm is currently visiting
        /// </summary>
        protected IVertex CurrentVertex { get; set; }


        /// <summary>
        /// Checks, whether the algorithm has 
        /// reached it's destination point or 
        /// whether it can go on the pathifing
        /// process
        /// </summary>
        /// <returns>true - if algorithm is done ot if it is 
        /// is not able to go on the pathfinding process</returns>
        protected virtual bool IsDestination()
        {
            return CurrentVertex.IsEqual(endPoints.Target)
                   || CurrentVertex.IsNullObject()
                   || isInterruptRequested;
        }

        /// <summary>
        /// Invokes <see cref="OnStarted"/> event
        /// </summary>
        /// <param name="e"></param>
        protected void RaiseOnAlgorithmStartedEvent(AlgorithmEventArgs e)
        {
            OnStarted?.Invoke(this, e);
        }

        /// <summary>
        /// Invokes <see cref="OnFinished"/> event
        /// </summary>
        /// <param name="e"></param>
        protected void RaiseOnAlgorithmFinishedEvent(AlgorithmEventArgs e)
        {
            OnFinished?.Invoke(this, e);
        }

        /// <summary>
        /// Invokes <see cref="OnVertexVisited"/> event
        /// </summary>
        /// <param name="e"></param>
        protected void RaiseOnVertexVisitedEvent(AlgorithmEventArgs e)
        {
            OnVertexVisited?.Invoke(this, e);
        }

        /// <summary>
        /// Invokes <see cref="OnVertexEnqueued"/> event
        /// </summary>
        /// <param name="e"></param>
        protected void RaiseOnVertexEnqueuedEvent(AlgorithmEventArgs e)
        {
            OnVertexEnqueued?.Invoke(this, e);
        }

        /// <summary>
        /// Prepares algorithm for pathfinding process
        /// </summary>
        protected virtual void PrepareForPathfinding()
        {
            if (graph.Contains(endPoints))
            {
                CurrentVertex = endPoints.Source;
                visitedVertices.Add(CurrentVertex);
                var args = CreateEventArgs(CurrentVertex);
                RaiseOnAlgorithmStartedEvent(args);
                return;
            }

            throw new ArgumentException($"{nameof(endPoints)} don't belong to {nameof(graph)}");
        }

        /// <summary>
        /// Completes pathfinding process
        /// </summary>
        protected virtual void CompletePathfinding()
        {
            var args = CreateEventArgs(CurrentVertex);
            RaiseOnAlgorithmFinishedEvent(args);
        }

        protected virtual AlgorithmEventArgs CreateEventArgs(IVertex vertex)
        {
            return new AlgorithmEventArgs(visitedVertices.Count, endPoints, vertex);
        }

        protected ICoordinate Position(IVertex vertex)
        {
            return vertex.Position;
        }

        public void Dispose()
        {
            Reset();
        }

        protected readonly IVisitedVertices visitedVertices;
        protected readonly ParentVertices parentVertices;
        protected IAccumulatedCosts accumulatedCosts;

        protected readonly IGraph graph;
        protected readonly IEndPoints endPoints;

        private bool isInterruptRequested;
    }
}