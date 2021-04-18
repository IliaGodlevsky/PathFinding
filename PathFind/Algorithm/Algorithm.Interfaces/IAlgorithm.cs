using Algorithm.Infrastructure.Handlers;
using Common.Interface;
using GraphLib.Interfaces;
using System;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// A base interface for all path finding algorithms
    /// </summary>
    public interface IAlgorithm : IInterruptable, IDisposable
    {
        /// <summary>
        /// Occurs when the algorithm starts pathfinding
        /// </summary>
        event AlgorithmEventHandler OnStarted;
        /// <summary>
        /// Occurs when the algorithm visits the next vertex
        /// </summary>
        event AlgorithmEventHandler OnVertexVisited;
        /// <summary>
        /// Occurs when the algorithm finishes pathfinding
        /// </summary>
        event AlgorithmEventHandler OnFinished;
        /// <summary>
        /// Occurs when the algorithm adds vertex to process queue
        /// </summary>
        event AlgorithmEventHandler OnVertexEnqueued;

        /// <summary>
        /// Starts path finding
        /// </summary>
        IGraphPath FindPath(IEndPoints endPoints);
    }
}
