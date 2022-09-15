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
    internal abstract class AlgorithmVertices : IVisualizationSlides<IVertex>, IExecutable<IAlgorithm<IGraphPath>>, IAlgorithmVertices
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentDictionary<ICoordinate, IVertex>> vertices;

        public AlgorithmVertices()
        {
            vertices = new ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentDictionary<ICoordinate, IVertex>>();
        }

        public virtual void Add(IAlgorithm<IGraphPath> algorithm, IVertex vertex)
        {
            vertices.TryGetOrAddNew(algorithm).TryAddOrUpdate(vertex.Position, vertex);
            Visualize(vertex.AsVisualizable());
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            vertices.TryRemove(algorithm, out _);
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm, IVertex vertex)
        {
            vertices.GetOrEmpty(algorithm).TryRemove(vertex.Position, out _);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            GetVertices(algorithm).OfType<IVisualizable>().ForEach(Visualize);
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm<IGraphPath> algorithm)
        {
            return (IReadOnlyCollection<IVertex>)vertices.GetOrEmpty(algorithm).Values;
        }

        protected abstract void Visualize(IVisualizable visualizable);
    }
}
