using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Visualization.Interfaces;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Abstractions
{
    internal abstract class GraphVertices<TVertex> : IVisualizationSlides<TVertex>, IExecutable<IAlgorithm<IGraphPath>>, IVisualizationStore<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentBag<TVertex>> vertices;

        public GraphVertices()
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