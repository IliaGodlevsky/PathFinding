using Algorithm.Delegates;
using Common.Interfaces;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.PathFindingAlgorithms.Interface
{
    /// <summary>
    /// A base interface of path finding algorithms
    /// </summary>
    public interface IAlgorithm : IDefault
    {
        event AlgorithmEventHanlder OnStarted;
        event Action<IVertex> OnVertexVisited;
        event AlgorithmEventHanlder OnFinished;
        event Action<IVertex> OnVertexEnqueued;

        IGraph Graph { get; }

        /// <summary>
        /// Finds path from start vertex to end vertex by definite rules
        /// </summary>
        void FindPath();
    }
}
