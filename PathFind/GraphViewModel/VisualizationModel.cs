using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphViewModel.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GraphViewModel
{
    public abstract class VisualizationModel : IModel
    {
        protected VisualizationModel(IGraph graph)
        {
            visitedVertices = new ConcurrentDictionary<IAlgorithm, List<IVertex>>();
            enqueuedVertices = new ConcurrentDictionary<IAlgorithm, List<IVertex>>();
            pathVertices = new Dictionary<IAlgorithm, List<IVertex>>();
            intermediate = new Dictionary<IAlgorithm, List<IVertex>>();
            source = new Dictionary<IAlgorithm, IVertex>();
            target = new Dictionary<IAlgorithm, IVertex>();
            this.graph = graph;
        }

        public void Clear()
        {
            pathVertices.Clear();
            visitedVertices.Clear();
            enqueuedVertices.Clear();
            source.Clear();
            target.Clear();
            intermediate.Clear();
        }

        public virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (CanVisualize(sender, e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsVisited();
                visitedVertices.TryGetOrAdd(algorithm).Add(e.Current);
                enqueuedVertices.TryGetOrAdd(algorithm).Remove(e.Current);
            }           
        }

        public virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (CanVisualize(sender, e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsEnqueued();
                enqueuedVertices.TryGetOrAdd(algorithm).Add(e.Current);
            }
        }

        public void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
        }

        public void AddEndPoints(IAlgorithm algorithm, IIntermediateEndPoints endPoints)
        {
            source[algorithm] = endPoints.Source;
            target[algorithm] = endPoints.Target;
            intermediate.TryGetOrAdd(algorithm).AddRange(endPoints.IntermediateVertices);
        }

        public void AddPathVertices(IAlgorithm algorithm, IGraphPath path)
        {
            pathVertices.TryGetOrAdd(algorithm).AddRange(path.Path);
        }

        public virtual void ShowAlgorithmVisualization(IAlgorithm algorithm)
        {
            graph.Refresh();
            foreach (IVisualizable vertex in enqueuedVertices.GetOrEmpty(algorithm)) vertex.VisualizeAsEnqueued();
            foreach (IVisualizable vertex in visitedVertices.GetOrEmpty(algorithm)) vertex.VisualizeAsVisited();
            foreach (IVisualizable vertex in pathVertices.GetOrEmpty(algorithm)) vertex.VisualizeAsPath();
            foreach (IVisualizable vertex in intermediate.GetOrEmpty(algorithm)) vertex.VisualizeAsIntermediate();
            (source.GetOrDefault(algorithm, () => NullVertex.Instance) as IVisualizable)?.VisualizeAsSource();
            (target.GetOrDefault(algorithm, () => NullVertex.Instance) as IVisualizable)?.VisualizeAsTarget();
        }

        public void Remove(IAlgorithm algorithm)
        {
            pathVertices.Remove(algorithm);
            visitedVertices.TryRemove(algorithm, out _);
            enqueuedVertices.TryRemove(algorithm, out _);
            source.Remove(algorithm);
            target.Remove(algorithm);
            intermediate.Remove(algorithm);
        }

        private bool IsEndPoints(IAlgorithm algorithm, IVertex vertex)
        {
            return source.GetOrDefault(algorithm, () => NullVertex.Instance).Equals(vertex)
                || target.GetOrDefault(algorithm, () => NullVertex.Instance).Equals(vertex)
                || intermediate.GetOrEmpty(algorithm).Contains(vertex);
        }

        private bool CanVisualize(object sender, AlgorithmEventArgs e, out IAlgorithm algorithm, out IVisualizable vertex)
        {
            algorithm = NullAlgorithm.Instance;
            vertex = NullVisualizable.Instance;
            if (e.Current is IVisualizable vert && sender is IAlgorithm algo 
                && !IsEndPoints(algo, e.Current))
            {
                algorithm = algo;
                vertex = vert;
                return true;               
            }

            return false;
        }

        protected readonly ConcurrentDictionary<IAlgorithm, List<IVertex>> visitedVertices;
        protected readonly ConcurrentDictionary<IAlgorithm, List<IVertex>> enqueuedVertices;
        protected readonly Dictionary<IAlgorithm, List<IVertex>> pathVertices;       
        protected readonly Dictionary<IAlgorithm, List<IVertex>> intermediate;
        protected readonly Dictionary<IAlgorithm, IVertex> source;
        protected readonly Dictionary<IAlgorithm, IVertex> target;
        protected readonly IGraph graph;
    }
}