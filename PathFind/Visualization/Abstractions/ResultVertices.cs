using Algorithm.Interfaces;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class ResultVertices : IVisualizationSlides<IVertex>, IExecutable<IAlgorithm<IGraphPath>>, IAlgorithmVertices
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentBag<IVertex>> vertices;

        public ResultVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentBag<IVertex>>();
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, IVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).Add(vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm<IGraphPath> algorithm)
        {
            return vertices.GetOrEmpty(algorithm);
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.GetOrEmpty(algorithm).OfType<IVisualizable>().ForEach(Visualize);
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}