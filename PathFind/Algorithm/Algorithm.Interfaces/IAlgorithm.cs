using Algorithm.Infrastructure.Handlers;
using Interruptable.Interface;
using System;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// Provides events and methods for 
    /// pathfinding algorithm
    /// </summary>
    public interface IAlgorithm : IInterruptableProcess, IInterruptable, IDisposable
    {
        event AlgorithmEventHandler VertexVisited;
        event AlgorithmEventHandler VertexEnqueued;

        IGraphPath FindPath();
    }
}
