using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GraphViewModel
{
    public abstract class VisualizationModel<TColor>
    {
        protected VisualizationModel(IGraph graph)
        {
            pathVertices = new Dictionary<IAlgorithm, List<IVertex>>();
            visitedVertices = new ConcurrentDictionary<IAlgorithm, List<IVertex>>();
            enqueuedVertices = new ConcurrentDictionary<IAlgorithm, List<IVertex>>();
            source = new Dictionary<IAlgorithm, IVertex>();
            target = new Dictionary<IAlgorithm, IVertex>();
            intermediate = new Dictionary<IAlgorithm, List<IVertex>>();
            previousColors = new Dictionary<IVertex, TColor>();
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
            previousColors.Clear();
        }

        public virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (e.Current is IVisualizable vertex && sender is IAlgorithm algorithm
                && !IsEndPoints(algorithm, e.Current))
            {
                vertex.VisualizeAsVisited();
                if (!visitedVertices.TryGetValue(algorithm, out _))
                {
                    visitedVertices.TryAdd(algorithm, new List<IVertex>());
                }
                visitedVertices[algorithm].Add(e.Current);
                if (enqueuedVertices.TryGetValue(algorithm, out var list))
                {
                    lock (list)
                    {
                        list.Remove(e.Current);
                    }
                }
            }           
        }

        public virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (e.Current is IVisualizable vertex
                && sender is IAlgorithm algorithm
                && !IsEndPoints(algorithm, e.Current))
            {
                vertex.VisualizeAsEnqueued();
                if (!enqueuedVertices.TryGetValue(algorithm, out _))
                {
                    enqueuedVertices.TryAdd(algorithm, new List<IVertex>());
                }
                enqueuedVertices[algorithm].Add(e.Current);
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
            if (!intermediate.TryGetValue(algorithm, out _))
            {
                intermediate.Add(algorithm, new List<IVertex>());
            }
            intermediate[algorithm].AddRange(endPoints.IntermediateVertices);
        }

        public void AddPathVertices(IAlgorithm algorithm, IGraphPath path)
        {
            if(!pathVertices.TryGetValue(algorithm, out _))
            {
                pathVertices.Add(algorithm, new List<IVertex>());
            }
            pathVertices[algorithm].AddRange(path.Path);
        }

        public virtual void ShowAlgorithmVisualization(IAlgorithm algorithm)
        {
            // TODO: save all current colors
            foreach (IVisualizable vertex in enqueuedVertices[algorithm]) vertex.VisualizeAsEnqueued();
            foreach (IVisualizable vertex in visitedVertices[algorithm]) vertex.VisualizeAsVisited();
            foreach (IVisualizable vertex in intermediate[algorithm]) vertex.VisualizeAsIntermediate();
            foreach (IVisualizable vertex in pathVertices[algorithm]) vertex.VisualizeAsPath();
            (source[algorithm] as IVisualizable)?.VisualizeAsSource();
            (target[algorithm] as IVisualizable)?.VisualizeAsTarget();
        }

        public virtual void HideVisualiztion()
        {
            // TODO: return previous colors
        }

        private bool IsEndPoints(IAlgorithm algorithm, IVertex vertex)
        {
            return source[algorithm].Equals(vertex) || target[algorithm].Equals(vertex)
                || intermediate[algorithm].Contains(vertex);
        }

        protected readonly Dictionary<IAlgorithm, List<IVertex>> pathVertices;
        protected readonly ConcurrentDictionary<IAlgorithm, List<IVertex>> visitedVertices;
        protected readonly ConcurrentDictionary<IAlgorithm, List<IVertex>> enqueuedVertices;
        protected readonly Dictionary<IAlgorithm, IVertex> source;
        protected readonly Dictionary<IAlgorithm, IVertex> target;
        protected readonly Dictionary<IAlgorithm, List<IVertex>> intermediate;
        protected readonly Dictionary<IVertex, TColor> previousColors;
        protected readonly IGraph graph;
    }
}