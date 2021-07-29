using Algorithm.Infrastructure.Handlers;
using Interruptable.Interface;
using System;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Provides events and methods for 
    /// pathfinding algorithm
    /// </summary>
    public interface IAlgorithm : IInterruptable, IDisposable
    {
        event AlgorithmEventHandler OnStarted;
        event AlgorithmEventHandler OnVertexVisited;
        event AlgorithmEventHandler OnVertexEnqueued;
        event AlgorithmEventHandler OnFinished;

        IGraphPath FindPath();
    }
}
