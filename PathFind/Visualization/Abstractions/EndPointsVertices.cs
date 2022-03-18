using Algorithm.Interfaces;
using Commands.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class EndPointsVertices : IVisualizationSlides, IExecutable<IAlgorithm>
    {
        private readonly ConcurrentDictionary<IAlgorithm, IVertex> vertices;

        public EndPointsVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm, IVertex>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            vertices[algorithm] = vertex;
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return new IVertex[] { vertices.GetOrNullVertex(algorithm) };
        }

        public void Remove(IAlgorithm algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Execute(IAlgorithm algorithm)
        {
            var vertex = vertices.GetOrNullVertex(algorithm).AsVisualizable();
            Visualize(vertex);
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}