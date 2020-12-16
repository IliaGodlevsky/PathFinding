using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;
using GraphLib.Vertex.Interface;
using System;

namespace Algorithm.Algorithms.Abstractions
{
    public sealed class DefaultAlgorithm : IAlgorithm
    {
        public event Action OnStarted;
        public event Action<IVertex> OnVertexVisited;
        public event Action OnFinished;
        public event Action<IVertex> OnVertexEnqueued;

        public IGraph Graph
        {
            get => new NullGraph();
            set => _ = value;
        }

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
            OnStarted?.Invoke();
            OnVertexVisited?.Invoke(new DefaultVertex());
            OnVertexEnqueued?.Invoke(new DefaultVertex());
            OnFinished?.Invoke();
        }
    }
}
