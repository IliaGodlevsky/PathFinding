using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Visualization.Interfaces;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Collections;
using Shared.Executable;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Visualization.Abstractions
{
    internal abstract class PathfindingRangeVertices<TVertex> : IVisualizationSlides<TVertex>, IExecutable<IAlgorithm<IGraphPath>>, IVisualizationStore<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, TVertex> vertices;

        public PathfindingRangeVertices()
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