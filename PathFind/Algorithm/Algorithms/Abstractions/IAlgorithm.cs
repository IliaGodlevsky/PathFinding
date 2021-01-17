using Algorithm.Handlers;
using Common.Interfaces;
using GraphLib.Graphs.Abstractions;

namespace Algorithm.Algorithms.Abstractions
{
    public interface IAlgorithm : IDefault
    {
        event AlgorithmEventHandler OnStarted;
        event AlgorithmEventHandler OnVertexVisited;
        event AlgorithmEventHandler OnFinished;
        event AlgorithmEventHandler OnVertexEnqueued;

        IGraph Graph { get; }

        void FindPath();
    }
}
