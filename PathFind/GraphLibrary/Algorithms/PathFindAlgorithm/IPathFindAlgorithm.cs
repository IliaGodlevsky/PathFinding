using GraphLibrary.AlgorithmArgs;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;

namespace GraphLibrary.Algorithm
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

        Graph Graph { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns> collection of path vertices </returns>
        IEnumerable<IVertex> FindPath();
    }
}
