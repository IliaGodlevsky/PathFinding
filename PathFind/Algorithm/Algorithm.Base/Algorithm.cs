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
using System.Linq;

namespace Algorithm.Base
{
    public abstract class Algorithm : IAlgorithm
    {
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;

        public abstract IGraphPath FindPath();

        public virtual void Interrupt()
        {
            isInterruptRequested = true;
            Interrupted?.Invoke(this, new ProcessEventArgs());
        }

        protected Algorithm(IGraph graph, IIntermediateEndPoints endPoints)
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
            isInterruptRequested = false;
        }

        protected IVertex CurrentVertex { get; set; }
        protected abstract IVertex NextVertex { get; }

        protected bool IsAbleToContinue => !CurrentVertex.IsNull() && !isInterruptRequested;

        protected virtual bool IsDestination(IEndPoints endPoints)
        {
            return endPoints.Target.IsEqual(CurrentVertex) || !IsAbleToContinue;
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
            if (!graph.Contains(endPoints))
            {
                throw new ArgumentException($"{nameof(endPoints)} don't belong to {nameof(graph)}");
            }
            Started?.Invoke(this, new ProcessEventArgs());
        }

        protected virtual void CompletePathfinding()
        {
            Finished?.Invoke(this, new ProcessEventArgs());
        }

        public void Dispose()
        {
            Started = null;
            Finished = null;
            VertexEnqueued = null;
            VertexVisited = null;
            Interrupted = null;
            Reset();
        }

        private bool isInterruptRequested;
        protected IAccumulatedCosts accumulatedCosts;
        protected readonly IVisitedVertices visitedVertices;
        protected readonly IParentVertices parentVertices;
        protected readonly IGraph graph;
        protected readonly IIntermediateEndPoints endPoints;
    }
}