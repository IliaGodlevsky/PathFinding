using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Infrastructure;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    internal sealed class CostVertices : IVisualizationSlides, IVisualization
    {
        private readonly ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<IVertex, IVertexCost>> verticesCosts;
        private readonly ConcurrentDictionary<IVertex, IVertexCost> actualVerticesCosts;

        public CostVertices()
        {
            verticesCosts = new ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<IVertex, IVertexCost>>();
            actualVerticesCosts = new ConcurrentDictionary<IVertex, IVertexCost>();
        }

        public void Add(IAlgorithm algorithm, IVertex vertex)
        {
            verticesCosts.TryGetOrAddNew(algorithm).TryAddOrUpdate(vertex, vertex.Cost);
            actualVerticesCosts.TryAdd(vertex, vertex.Cost);
        }

        public void ReturnActualCosts()
        {
            actualVerticesCosts.ForEach(item => item.Key.Cost = item.Value);
        }

        public void Clear()
        {
            verticesCosts.Clear();
            ReturnActualCosts();
            actualVerticesCosts.Clear();
        }

        public IReadOnlyCollection<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return (IReadOnlyCollection<IVertex>)verticesCosts.GetOrEmpty(algorithm).Keys;
        }

        public void Remove(IAlgorithm algorithm)
        {
            verticesCosts.TryRemove(algorithm, out _);
        }

        public void Visualize(IAlgorithm algorithm)
        {
            verticesCosts.GetOrEmpty(algorithm).ForEach(item => item.Key.Cost = item.Value);
        }

        public void OnCostChanged(object sender, CostChangedEventArgs e)
        {
            actualVerticesCosts.TryAddOrUpdate(e.Vertex, e.Cost);
        }
    }
}
