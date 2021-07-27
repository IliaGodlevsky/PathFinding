using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Algorithm.Сompanions;
using Algorithm.Сompanions.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using NullObject.Extensions;
using System;

namespace Algorithm.Base
{
    /// <summary>
    /// A base class for all pathfinding algorithms.
    /// This is an abstract class
    /// </summary>
    public abstract class Algorithm : IAlgorithm
    {
        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;
        public event InterruptEventHanlder OnInterrupted;

        public abstract IGraphPath FindPath();

        public virtual void Interrupt()
        {
            IsInterruptRequested = true;
            OnInterrupted?.Invoke(this, new InterruptEventArgs());
        }

        protected Algorithm(IGraph graph, IEndPoints endPoints)
        {
            visitedVertices = new VisitedVertices();
            parentVertices = new ParentVertices();
            this.graph = graph;
            this.endPoints = endPoints;
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
            IsInterruptRequested = false;
        }

        protected IVertex CurrentVertex { get; set; }

        private bool IsInterruptRequested { get; set; }

        protected virtual bool IsDestination()
        {
            return endPoints.Target.IsEqual(CurrentVertex)
                || CurrentVertex.IsNull()
                || IsInterruptRequested;
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
        protected readonly IParentVertices parentVertices;
        protected IAccumulatedCosts accumulatedCosts;

        protected readonly IGraph graph;
        protected readonly IEndPoints endPoints;
    }
}