using GraphLibrary.AlgorithmArgs;
using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;

namespace GraphLibrary.Algorithm
{
    public delegate void AlgorithmEventHanlder(object sender, AlgorithmEventArgs e);

    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        event AlgorithmEventHanlder OnAlgorithmStarted;
        event Action<IVertex> OnVertexVisited;
        event AlgorithmEventHanlder OnAlgorithmFinished;

        Graph Graph { get; set; }
        IEnumerable<IVertex> FindDestionation();
    }
}
