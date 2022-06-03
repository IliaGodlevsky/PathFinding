using Algorithm.Interfaces;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class ResultVertices : IVisualizationSlides<IVertex>, IExecutable<IAlgorithm>, IAlgorithmVertices
    {
        private readonly ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>> vertices;

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

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return vertices.GetOrEmpty(algorithm);
        }

        public void Remove(IAlgorithm algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Execute(IAlgorithm algorithm)
        {
            vertices.GetOrEmpty(algorithm).OfType<IVisualizable>().ForEach(Visualize);
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}