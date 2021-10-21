using Algorithm.Infrastructure;
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
using Interruptable.Interface;
using NullObject.Extensions;
using System;

namespace Algorithm.Base
{
    /// <summary>
    /// A base algorithm for all pathfinding algorithms.
    /// This class is abstract
    /// </summary>
    /// <remarks>Try to wait the algorithm finishes pathfinding and only then
    /// call <see cref="FindPath"/> method one more time</remarks>
    public abstract class PathfindingAlgorithm
        : IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        /// <summary>
        /// Occurres, when the algorithm visites a vertex
        /// </summary>
        public event AlgorithmEventHandler VertexVisited;
        /// <summary>
        /// Occurres, when the algorithm enqueues a vertex
        /// for the future processing
        /// </summary>
        public event AlgorithmEventHandler VertexEnqueued;
        /// <summary>
        /// Occures, when the algorithm starts the pathfinding
        /// </summary>
        public event ProcessEventHandler Started;
        /// <summary>
        /// Occurres, when the algorithm finishes the pathfinding
        /// </summary>
        public event ProcessEventHandler Finished;
        /// <summary>
        /// Occurres, when the algorithm is required to interrupt
        /// </summary>
        public event ProcessEventHandler Interrupted;

        /// <summary>
        /// When overriden in derived class, 
        /// finds the cheapest path in the graph
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EndPointsNotFromCurrentGraphException>">when graph
        /// doesn't contains end points</exception>
        public abstract IGraphPath FindPath();

        /// <summary>
        /// Interruptes the algorithm
        /// </summary>
        /// <remarks>This method doesn't stops the algorithm
        /// immidietly. The algorithm will complete the iteration
        /// and then will stop.</remarks>
        public void Interrupt()
        {
            IsInterruptRequested = true;
            Interrupted?.Invoke(this, new ProcessEventArgs());
        }

        protected PathfindingAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
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

        protected bool IsAbleToContinue => !CurrentVertex.IsNull() && !IsInterruptRequested;

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
            if (!graph.ContainsReferences(endPoints))
            {
                throw new EndPointsNotFromCurrentGraphException(graph, endPoints);
            }
            Reset();
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

        private bool IsInterruptRequested { get; set; }

        protected IAccumulatedCosts accumulatedCosts;
        protected readonly IVisitedVertices visitedVertices;
        protected readonly IParentVertices parentVertices;
        protected readonly IGraph graph;
        protected readonly IIntermediateEndPoints endPoints;
    }
}