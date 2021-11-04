using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GraphViewModel.Visualizations
{
    internal abstract class ResultVertices : IProcessedVertices, IVisualization
    {
        public ResultVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).Add(vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public IEnumerable<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return vertices.GetOrEmpty(algorithm);
        }

        public void Remove(IAlgorithm algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        protected abstract void Visualize(IVisualizable visualizable);

        public void Visualize(IAlgorithm algorithm)
        {
            vertices.GetOrEmpty(algorithm).OfType<IVisualizable>().ForEach(Visualize);
        }

        private readonly ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>> vertices;
    }
}
