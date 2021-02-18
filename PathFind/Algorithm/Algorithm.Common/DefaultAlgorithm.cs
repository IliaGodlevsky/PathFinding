using Algorithm.Infrastructure.EventArguments;
using Algorithm.Infrastructure.Handlers;
using Algorithm.Interfaces;
using Common.Attributes;
using GraphLib.Common.NullObjects;
using GraphLib.Interface;
using System.Threading.Tasks;

namespace Algorithm.Common
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

        public DefaultAlgorithm()
        {

        }

        public DefaultAlgorithm(IGraph graph)
        {
            Graph = graph;
        }

        public IGraphPath FindPath(IEndPoints endpoints)
        {
            OnStarted?.Invoke(this, new AlgorithmEventArgs());
            OnVertexVisited?.Invoke(this, new AlgorithmEventArgs());
            OnVertexEnqueued?.Invoke(this, new AlgorithmEventArgs());
            OnFinished?.Invoke(this, new AlgorithmEventArgs());
            return new NullGraphPath();
        }

        public void Reset()
        {

        }

        public async Task<IGraphPath> FindPathAsync(IEndPoints endPoints)
        {
            return await Task.Run(() => FindPath(endPoints));
        }
    }
}
