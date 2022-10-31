using Algorithm.Interfaces;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class AlgorithmVertices<TVertex> : IVisualizationSlides<TVertex>, IExecutable<IAlgorithm<IGraphPath>>, IAlgorithmVertices<TVertex>
        where TVertex : IVisualizable, IVertex
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentDictionary<ICoordinate, TVertex>> vertices;

        public AlgorithmVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentDictionary<ICoordinate, TVertex>>();
        }

        public virtual void Add(IAlgorithm<IGraphPath> algorithm, TVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).TryAddOrUpdate(vertex.Position, vertex);
            Visualize(vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm, TVertex vertex)
        {
            vertices.GetOrEmpty(algorithm).TryRemove(vertex.Position, out _);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            GetVertices(algorithm).ForEach(item => Visualize(item));
        }

        public IReadOnlyCollection<TVertex> GetVertices(IAlgorithm<IGraphPath> algorithm)
        {
            return (IReadOnlyCollection<TVertex>)vertices.GetOrEmpty(algorithm).Values;
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}
