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
        event AlgorithmEventHandler Started;
        event AlgorithmEventHandler VertexVisited;
        event AlgorithmEventHandler VertexEnqueued;
        event AlgorithmEventHandler Finished;

        IGraphPath FindPath();
    }
}
