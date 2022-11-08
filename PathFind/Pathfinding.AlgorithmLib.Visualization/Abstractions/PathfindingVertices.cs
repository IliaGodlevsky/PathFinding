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
    internal abstract class PathfindingVertices<TVertex> : IVisualizationSlides<TVertex>, IExecutable<IAlgorithm<IGraphPath>>, IVisualizationStore<TVertex>
        where TVertex : IVisualizable, IVertex
    {
        private readonly ConcurrentDictionary<IAlgorithm<IGraphPath>, ConcurrentDictionary<ICoordinate, TVertex>> vertices;

        public PathfindingVertices()
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
