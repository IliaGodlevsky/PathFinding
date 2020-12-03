using Algorithm.Delegates;
using Common.Interfaces;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.Algorithms.Abstractions
{
    public interface IAlgorithm : IDefault
    {
        event AlgorithmEventHanlder OnStarted;
        event Action<IVertex> OnVertexVisited;
        event AlgorithmEventHanlder OnFinished;
        event Action<IVertex> OnVertexEnqueued;

        IGraph Graph { get; }

        void FindPath();
    }
}
