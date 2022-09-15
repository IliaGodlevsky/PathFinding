using Algorithm.Interfaces;
using Commands.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class EndPointsVertices : IVisualizationSlides<IVertex>, IExecutable<IAlgorithm<IGraphPath>>, IAlgorithmVertices
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, IVertex> vertices;

        public EndPointsVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm<IGraphPath>, IVertex>();
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, IVertex vertex)
        {
            vertices[algorithm] = vertex;
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm<IGraphPath> algorithm)
        {
            return new IVertex[] { vertices.GetOrNullVertex(algorithm) };
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            var vertex = vertices.GetOrNullVertex(algorithm).AsVisualizable();
            Visualize(vertex);
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}