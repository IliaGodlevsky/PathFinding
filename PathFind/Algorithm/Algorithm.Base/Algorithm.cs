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
    public abstract class Algorithm : IAlgorithm
    {
        public event AlgorithmEventHandler Started;
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler Finished;
        public event AlgorithmEventHandler VertexEnqueued;
        public event InterruptEventHanlder Interrupted;

        public abstract IGraphPath FindPath();

        public virtual void Interrupt()
        {
            IsInterruptRequested = true;
            Interrupted?.Invoke(this, new InterruptEventArgs());
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
            visitedVertices.Clear();
            parentVertices.Clear();
            IsInterruptRequested = false;
        }

        protected IVertex CurrentVertex { get; set; }
        protected abstract IVertex NextVertex { get; }

        private bool IsInterruptRequested { get; set; }

        protected virtual bool IsDestination()
        {
            return endPoints.Target.IsEqual(CurrentVertex)
                || CurrentVertex.IsNull()
                || IsInterruptRequested;
        }

        protected void RaiseStarted(AlgorithmEventArgs e) 
        { 
            Started?.Invoke(this, e); 
        }

        protected void RaiseFinished(AlgorithmEventArgs e) 
        { 
            Finished?.Invoke(this, e);
        }

        protected void RaiseVertexVisited(AlgorithmEventArgs e) 
        {
            VertexVisited?.Invoke(this, e);
        }

        protected void RaiseVertexEnqueued(AlgorithmEventArgs e) 
        {
            VertexEnqueued?.Invoke(this, e);
        }

        protected virtual void PrepareForPathfinding()
        {
            if (graph.Contains(endPoints))
            {
                CurrentVertex = endPoints.Source;
                visitedVertices.Add(CurrentVertex);
                RaiseStarted(CreateEventArgs(CurrentVertex));
                return;
            }
            throw new ArgumentException($"{nameof(endPoints)} don't belong to {nameof(graph)}");
        }

        protected virtual void CompletePathfinding() 
        { 
            RaiseFinished(CreateEventArgs(CurrentVertex)); 
        }

        protected virtual AlgorithmEventArgs CreateEventArgs(IVertex vertex)
        { 
            return new AlgorithmEventArgs(visitedVertices.Count, vertex); 
        }

        protected ICoordinate Position(IVertex vertex) => vertex.Position;

        public void Dispose()
        {
            Started = null;
            Finished = null;
            VertexEnqueued = null;
            VertexVisited = null;
            Interrupted = null;
            Reset();
        }

        protected readonly IVisitedVertices visitedVertices;
        protected readonly IParentVertices parentVertices;
        protected IAccumulatedCosts accumulatedCosts;

        protected readonly IGraph graph;
        protected readonly IEndPoints endPoints;
    }
}