using Algorithm.Interfaces;
using Commands.Interfaces;
using Common.ReadOnly;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class EndPointsVertices<TVertex> : IVisualizationSlides<TVertex>, IExecutable<IAlgorithm<IGraphPath>>, IAlgorithmVertices<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, TVertex> vertices;

        public EndPointsVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm<IGraphPath>, TVertex>();
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, TVertex vertex)
        {
            vertices[algorithm] = vertex;
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public IReadOnlyCollection<TVertex> GetVertices(IAlgorithm<IGraphPath> algorithm)
        {
            if (vertices.TryGetValue(algorithm, out var vertex))
            {
                return new ReadOnlyList<TVertex>(vertex);
            }
            return ReadOnlyList<TVertex>.Empty;
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            if (vertices.TryGetValue(algorithm, out var vertex))
            {
                Visualize(vertex);
            }
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}