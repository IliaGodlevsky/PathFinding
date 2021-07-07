using Algorithm.Infrastructure.Handlers;
using AssembleClassesLib.Attributes;
using Common.Interface;
using System;

namespace Algorithm.Interfaces
{

    /// <summary>
    /// Provides events and methods for 
    /// pathfinding algorithm
    /// </summary>
    [NotLoadable]
    public interface IAlgorithm : IInterruptable, IDisposable
    {
        /// <summary>
        /// Occurs when the algorithm starts path finding
        /// </summary>
        event AlgorithmEventHandler OnStarted;
        /// <summary>
        /// Occurs when the algorithm visits the next vertex
        /// </summary>
        event AlgorithmEventHandler OnVertexVisited;
        /// <summary>
        /// Occurs when the algorithm finishes path finding
        /// </summary>
        event AlgorithmEventHandler OnFinished;
        /// <summary>
        /// Occurs when the algorithm adds vertex to process queue
        /// </summary>
        event AlgorithmEventHandler OnVertexEnqueued;

        /// <summary>
        /// Starts path finding
        /// </summary>
        IGraphPath FindPath();
    }
}
