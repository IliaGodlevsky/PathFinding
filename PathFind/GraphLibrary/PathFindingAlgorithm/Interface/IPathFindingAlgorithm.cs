using GraphLibrary.EventArguments;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;

namespace GraphLibrary.PathFindingAlgorithm.Interface
{
    public delegate void AlgorithmEventHanlder(object sender, AlgorithmEventArgs e);

    /// <summary>
    /// A base interface of path finding algorithms
    /// </summary>
    public interface IPathFindingAlgorithm
    {
        event AlgorithmEventHanlder OnStarted;
        event Action<IVertex> OnVertexVisited;
        event AlgorithmEventHanlder OnFinished;
        event Action<IVertex> OnEnqueued;

        IGraph Graph { get; set; }
        /// <summary>
        /// Finds path from start vertex to end vertex by definite rules
        /// </summary>
        /// <returns> collection of path vertices </returns>
        IEnumerable<IVertex> FindPath();
    }
}
