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
    /// <remarks>Do not use the same instance of algorithm untill it stops to 
    /// find the path. And only then use it again. But it is better not to use
    /// the same instance twice. Create a new one instead.</remarks>
    public abstract class PathfindingAlgorithm
        : IAlgorithm, IInterruptableProcess, IInterruptable, IDisposable
    {
        public event AlgorithmEventHandler VertexVisited;
        public event AlgorithmEventHandler VertexEnqueued;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;
        public event ProcessEventHandler Interrupted;

        public bool IsInProcess { get; private set; }

        /// <summary>
        /// When overriden in derived class, 
        /// finds the cheapest path in the graph
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EndPointsNotFromCurrentGraphException>">when graph
        /// doesn't contains end points</exception>
        public abstract IGraphPath FindPath();

        /// <summary>
        /// Iterrupts the algorithm and stops its activity
        /// The algorithm does not stop immidietly, 
        /// but it performs the current iteration
        /// and only then it will be stopped
        /// </summary>
        /// <remarks>The algorithm does not stop immidietly, 
        /// but it performs the current iteration
        /// and only then it will be stopped</remarks>
        public void Interrupt()
        {
            IsInterruptRequested = true;
            IsInProcess = false;
            Interrupted?.Invoke(this, new ProcessEventArgs());
        }

        /// <summary>
        /// Initialization a new instance of <see cref="PathfindingAlgorithm"/>
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="endPoints"></param>
        protected PathfindingAlgorithm(IEndPoints endPoints)
        {
            visitedVertices = new VisitedVertices();
            parentVertices = new ParentVertices();
            this.endPoints = endPoints;
        }

        /// <summary>
        /// Removes all activity of the
        /// algorithm so that you can use it again
        /// </summary>
        protected virtual void Reset()
        {
            visitedVertices.Clear();
            parentVertices.Clear();
            IsInterruptRequested = false;
        }

        /// <summary>
        /// The vertex that is currently being processed by the algorithm
        /// </summary>
        protected IVertex CurrentVertex { get; set; }

        /// <summary>
        /// The vertex that is next to be processed
        /// </summary>
        protected abstract IVertex NextVertex { get; }

        /// <summary>
        /// Determines whether the algorithm is able to continue 
        /// the pathfinding process
        /// algorithm terminated prematurely
        /// </summary>
        protected bool IsTerminatedPrematurely => !CurrentVertex.IsNull() && !IsInterruptRequested;

        /// <summary>
        /// Determines, whether the algorithm has reached the target vertex
        /// or whether it is able to continue pathfinding
        /// </summary>
        /// <param name="endPoints"></param>
        /// <returns></returns>
        protected virtual bool IsDestination(IEndPoints endPoints)
        {
            return endPoints.Target.IsEqual(CurrentVertex) || !IsTerminatedPrematurely;
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
            Reset();
            IsInProcess = true;
            Started?.Invoke(this, new ProcessEventArgs());
        }

        protected virtual void CompletePathfinding()
        {
            IsInProcess = true;
            Finished?.Invoke(this, new ProcessEventArgs());
        }

        /// <summary>
        /// Resets the algorithm and removes all 
        /// subscribers from its events
        /// </summary>
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

        protected readonly IVisitedVertices visitedVertices;
        protected readonly IParentVertices parentVertices;
        protected readonly IEndPoints endPoints;
    }
}