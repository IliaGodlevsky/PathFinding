using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class AlgorithmVertices : IVertices, IVisualization
    {
        public AlgorithmVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<ICoordinate, IVertex>>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).TryAddOrUpdate(vertex.Position, vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public void Remove(IAlgorithm algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Remove(IAlgorithm algorithm, IVertex vertex)
        {
            vertices.GetOrEmpty(algorithm).TryRemove(vertex.Position, out _);
        }

        public void Visualize(IAlgorithm algorithm)
        {
            GetVertices(algorithm)
                .OfType<IVisualizable>()
                .ForEach(Visualize);
        }

        public IEnumerable<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return vertices.GetOrEmpty(algorithm).Values;
        }

        protected abstract void Visualize(IVisualizable visualizable);

        private readonly ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<ICoordinate, IVertex>> vertices;
    }
}
