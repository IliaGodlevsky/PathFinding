using Algorithm.Infrastructure.Handlers;
using GraphLib.Interface;

namespace Algorithm.Interfaces
{
    /// <summary>
    /// A base interface for all the pathfinding algorithms
    /// </summary>
    public interface IAlgorithm
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
        /// A graph, where the pathfinding performes
        /// </summary>
        IGraph Graph { get; set; }

        /// <summary>
        /// Starts pathfinding
        /// </summary>
        IGraphPath FindPath(IEndPoints endPoints);

        void Reset();
    }
}
