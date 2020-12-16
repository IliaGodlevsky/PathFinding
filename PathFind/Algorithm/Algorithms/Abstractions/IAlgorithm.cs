using Common.Interfaces;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.Algorithms.Abstractions
{
    public interface IAlgorithm : IDefault
    {
        event Action OnStarted;
        event Action<IVertex> OnVertexVisited;
        event Action OnFinished;
        event Action<IVertex> OnVertexEnqueued;

        IGraph Graph { get; }

        void FindPath();
    }
}
