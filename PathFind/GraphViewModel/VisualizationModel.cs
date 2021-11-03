using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel.Extensions;
using GraphViewModel.Interfaces;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;

namespace GraphViewModel
{
    public abstract class VisualizationModel : IModel
    {
        protected VisualizationModel(IGraph graph)
        {
            visitedVertices = new ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<ICoordinate, IVertex>>();
            enqueuedVertices = new ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<ICoordinate, IVertex>>();
            pathVertices = new ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>>();
            intermediate = new ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>>();
            source = new ConcurrentDictionary<IAlgorithm, IVertex>();
            target = new ConcurrentDictionary<IAlgorithm, IVertex>();
            dictionaries = new IDictionary[] { visitedVertices, enqueuedVertices, pathVertices, intermediate, source, target };
            this.graph = graph;
        }

        public void Clear()
        {
            dictionaries.ForEach(dict => dict.Clear());
        }

        public void Remove(IAlgorithm algorithm)
        {
            dictionaries.ForEach(dict => dict.Remove(algorithm));
        }

        public virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (this.CanVisualize(sender, e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsVisited();
                visitedVertices
                    .TryGetOrAddNew(algorithm)
                    .TryAddOrUpdate(e.Current.Position, e.Current);
            }
        }

        public virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (this.CanVisualize(sender, e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsEnqueued();
                enqueuedVertices
                    .TryGetOrAddNew(algorithm)
                    .TryAddOrUpdate(e.Current.Position, e.Current);
            }
        }

        public virtual void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
        }

        public void AddEndPoints(IAlgorithm algorithm, IIntermediateEndPoints endPoints)
        {
            source[algorithm] = endPoints.Source;
            target[algorithm] = endPoints.Target;
            intermediate.TryGetOrAddNew(algorithm).AddRange(endPoints.IntermediateVertices);
        }

        public void AddPathVertices(IAlgorithm algorithm, IGraphPath path)
        {
            pathVertices.TryGetOrAddNew(algorithm).AddRange(path.Path);
        }

        public virtual void ShowAlgorithmVisualization(IAlgorithm algorithm)
        {
            graph.Refresh();
            foreach (IVisualizable vertex in enqueuedVertices.GetOrEmpty(algorithm).Values) vertex?.VisualizeAsEnqueued();
            foreach (IVisualizable vertex in visitedVertices.GetOrEmpty(algorithm).Values) vertex?.VisualizeAsVisited();
            foreach (IVisualizable vertex in pathVertices.GetOrEmpty(algorithm)) vertex?.VisualizeAsPath();
            foreach (IVisualizable vertex in intermediate.GetOrEmpty(algorithm)) vertex?.VisualizeAsIntermediate();
            (source.GetOrNullVertex(algorithm) as IVisualizable)?.VisualizeAsSource();
            (target.GetOrNullVertex(algorithm) as IVisualizable)?.VisualizeAsTarget();
        }

        internal protected bool IsEndPoints(IAlgorithm algorithm, IVertex vertex)
        {
            return source.GetOrNullVertex(algorithm).Equals(vertex)
                || target.GetOrNullVertex(algorithm).Equals(vertex)
                || intermediate.GetOrEmpty(algorithm).Contains(vertex);
        }

        protected readonly ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<ICoordinate, IVertex>> visitedVertices;
        protected readonly ConcurrentDictionary<IAlgorithm, ConcurrentDictionary<ICoordinate, IVertex>> enqueuedVertices;
        protected readonly ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>> pathVertices;
        protected readonly ConcurrentDictionary<IAlgorithm, ConcurrentBag<IVertex>> intermediate;
        protected readonly ConcurrentDictionary<IAlgorithm, IVertex> source;
        protected readonly ConcurrentDictionary<IAlgorithm, IVertex> target;
        protected readonly IDictionary[] dictionaries;
        protected readonly IGraph graph;
    }
}