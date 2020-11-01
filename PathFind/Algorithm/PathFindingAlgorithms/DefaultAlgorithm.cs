using System;
using GraphLib.Vertex.Interface;
using GraphLib.Vertex;
using Algorithm.EventArguments;
using Algorithm.PathFindingAlgorithms.Interface;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;

namespace Algorithm.PathFindingAlgorithms
{
    public sealed class DefaultAlgorithm : IPathFindingAlgorithm
    {
        public DefaultAlgorithm()
        {

        }

        public IGraph Graph { get => new DefaultGraph(); set => _ = value; }

        public bool IsDefault => true;

        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnEnqueued;

        public void FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(new DefaultVertex());
            OnFinished?.Invoke(this, new AlgorithmEventArgs());
            OnEnqueued?.Invoke(new DefaultVertex());
        }
    }
}
