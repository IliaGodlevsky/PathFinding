using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Visualization.Extensions;
using Visualization.Interfaces;

namespace Visualization.Realizations
{
    public abstract class PathfindingVisualization : IVertices, IVisualization
    {
        protected PathfindingVisualization(IGraph graph)
        {
            visited = new VisitedVertices();
            enqueued = new EnqueuedVertices();
            path = new PathVertices();
            intermediate = new IntermediateVertices();
            source = new SourceVertices();
            target = new TargetVertices();
            visualizations = new CompositeVisualization(graph, enqueued, visited, path, intermediate, source, target);
            vertices = new IVertices[] { visited, enqueued, path, intermediate, source, target };
        }

        public void Clear()
        {
            vertices.ForEach(processed => processed.Clear());
        }

        public void Remove(IAlgorithm algorithm)
        {
            vertices.ForEach(processed => processed.Remove(algorithm));
        }

        public void Visualize(IAlgorithm algorithm)
        {
            visualizations.Visualize(algorithm);
        }

        public virtual void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.Finished += OnAlgorithmFinished;
        }

        public void AddEndPoints(IAlgorithm algorithm, IEndPoints endPoints)
        {
            source.Add(algorithm, endPoints.Source);
            target.Add(algorithm, endPoints.Target);
            intermediate.AddRange(algorithm, endPoints.GetIntermediates());
        }

        public void AddPathVertices(IAlgorithm algorithm, IGraphPath grapPath)
        {
            path.AddRange(algorithm, grapPath.Path);
        }

        public IEnumerable<IVertex> GetVertices(IAlgorithm algorithm)
        {
            return vertices.SelectMany(processed => processed.GetVertices(algorithm));
        }

        public void Add(IAlgorithm algorithm, IVertex vertex) { }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (sender.CanBeVisualized(e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsVisited();
                visited.Add(algorithm, e.Current);
            }
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (sender.CanBeVisualized(e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsEnqueued();
                enqueued.Add(algorithm, e.Current);
            }
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (sender is PathfindingAlgorithm algorithm)
            {
                enqueued.RemoveRange(algorithm, visited.GetVertices(algorithm));
                algorithm.VertexVisited -= OnVertexVisited;
                algorithm.VertexEnqueued -= OnVertexEnqueued;
                algorithm.Finished -= OnAlgorithmFinished;
            }
        }

        private readonly VisitedVertices visited;
        private readonly EnqueuedVertices enqueued;
        private readonly PathVertices path;
        private readonly IntermediateVertices intermediate;
        private readonly SourceVertices source;
        private readonly TargetVertices target;
        private readonly IVisualization visualizations;
        private readonly IVertices[] vertices;
    }
}