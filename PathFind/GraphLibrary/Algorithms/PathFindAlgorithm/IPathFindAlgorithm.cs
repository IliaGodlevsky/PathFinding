using GraphLibrary.Collection;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Algorithm
{
    public delegate void AlgorithmEventHanlder();

    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        event AlgorithmEventHanlder OnAlgorithmStarted;
        event Action<IVertex> OnVertexVisited;
        event AlgorithmEventHanlder OnAlgorithmFinished;

        Graph Graph { get; set; }
        void FindDestionation();
    }
}
