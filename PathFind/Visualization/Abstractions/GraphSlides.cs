using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Infrastructure.EventArguments;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Abstractions
{
    internal abstract class GraphSlides<T> : IVisualizationSlides, IVisualization
    {
        protected GraphSlides()
        {
            verticesStates = new ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<IVertex, T>>();
            actualVerticesStates = new ConcurrentDictionary<IVertex, T>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            verticesStates.TryGetOrAddNew(algorithm).TryAddOrUpdate(vertex, GetStored(vertex));
            actualVerticesStates.TryAdd(vertex, GetActual(vertex));
        }

        public void RestoreActualState()
        {
            actualVerticesStates.ForEach(item => SetStored(item.Key, item.Value));
        }

        public void Clear()
        {
            verticesStates.Clear();
            RestoreActualState();
            actualVerticesStates.Clear();
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return (IReadOnlyCollection<IVertex>)verticesStates.GetOrEmpty(algorithm).Keys;
        }

        public void Remove(IAlgorithm algorithm)
        {
            verticesStates.TryRemove(algorithm, out _);
        }

        public void Visualize(IAlgorithm algorithm)
        {
            verticesStates.GetOrEmpty(algorithm).ForEach(item => SetStored(item.Key, item.Value));
        }

        public void OnStateChanged(object sender, BaseVertexChangedEventArgs<T> e)
        {
            actualVerticesStates.TryAddOrUpdate(e.Vertex, e.Changed);
        }

        protected abstract T GetStored(IVertex vertex);
        protected abstract void SetStored(IVertex vertex, T stored);
        protected abstract T GetActual(IVertex vertex);

        private readonly ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<IVertex, T>> verticesStates;
        private readonly ConcurrentDictionary<IVertex, T> actualVerticesStates;
    }
}
