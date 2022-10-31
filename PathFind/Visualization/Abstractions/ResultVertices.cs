using Algorithm.Interfaces;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class ResultVertices<TVertex> : IVisualizationSlides<TVertex>, IExecutable<IAlgorithm<IGraphPath>>, IAlgorithmVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentBag<TVertex>> vertices;

        public ResultVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentBag<TVertex>>();
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, TVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).Add(vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public IReadOnlyCollection<TVertex> GetVertices(IAlgorithm<IGraphPath> algorithm)
        {
            return vertices.GetOrEmpty(algorithm);
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.GetOrEmpty(algorithm).ForEach(item => Visualize(item));
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}