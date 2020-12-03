using System;
using GraphLib.Vertex.Interface;
using GraphLib.Vertex;
using Algorithm.EventArguments;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using Algorithm.Delegates;

namespace Algorithm.Algorithms.Abstractions
{
    internal sealed class DefaultAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHanlder OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event AlgorithmEventHanlder OnFinished;
        public event Action<IVertex> OnVertexEnqueued;

        public IGraph Graph { get => new DefaultGraph(); set => _ = value; }

        public bool IsDefault => true;

        public DefaultAlgorithm()
        {

        }

        public DefaultAlgorithm(IGraph graph)
        {
            Graph = graph;
        }

        public void FindPath()
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(new DefaultVertex());
            OnVertexEnqueued?.Invoke(new DefaultVertex());
            OnFinished?.Invoke(this, new AlgorithmEventArgs());
        }
    }
}
