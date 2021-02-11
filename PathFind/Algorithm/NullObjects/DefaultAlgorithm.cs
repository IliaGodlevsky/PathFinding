using Algorithm.Attributes;
using Algorithm.EventArguments;
using Algorithm.Handlers;
using Algorithm.Interface;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System.Collections.Generic;

namespace Algorithm.NullObjects
{
    [Filterable]
    public sealed class DefaultAlgorithm : IAlgorithm
    {
        public event AlgorithmEventHandler OnStarted;
        public event AlgorithmEventHandler OnVertexVisited;
        public event AlgorithmEventHandler OnFinished;
        public event AlgorithmEventHandler OnVertexEnqueued;

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

        public IGraphPath FindPath(IEndPoints endpoints)
        {
            var parentVertices = new Dictionary<ICoordinate, IVertex>();
            OnStarted?.Invoke(this, new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(this, new AlgorithmEventArgs());
            OnVertexEnqueued?.Invoke(this, new AlgorithmEventArgs());
            OnFinished?.Invoke(this, new AlgorithmEventArgs());
            return new GraphPath(parentVertices, endpoints);
        }

        public void Reset()
        {

        }
    }
}
