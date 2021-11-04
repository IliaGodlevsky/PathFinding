using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel.Extensions;
using GraphViewModel.Interfaces;
using GraphViewModel.Visualizations;
using System;

namespace GraphViewModel
{
    public abstract class VisualizationModel : IModel
    {
        protected VisualizationModel(IGraph graph)
        {
            visited = new VisitedVertices();
            enqueued = new EnqueuedVertices();
            path = new PathVertices();
            intermediate = new IntermediateVertices();
            source = new SourceVertices();
            target = new TargetVertices();
            visualizations = new CompositeVisualization(graph, enqueued, visited, path, intermediate, source, target);
            processedVertices = new IProcessedVertices[] { visited, enqueued, path, intermediate, source, target };
        }

        public void Clear() => processedVertices.ForEach(processed => processed.Clear());
        public void Remove(IAlgorithm algorithm) => processedVertices.ForEach(processed => processed.Remove(algorithm));
        public void ShowAlgorithmVisualization(IAlgorithm algorithm) => visualizations.Visualize(algorithm);

        public virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (this.CanVisualize(sender, e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsVisited();
                visited.Add(algorithm, e.Current);
            }
        }

        public virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (this.CanVisualize(sender, e, out var algorithm, out var vertex))
            {
                vertex.VisualizeAsEnqueued();
                enqueued.Add(algorithm, e.Current);
            }
        }

        public virtual void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.Finished += OnAlgorithmFinished;
        }

        public virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (sender is IAlgorithm algorithm)
            {
                enqueued.RemoveRange(algorithm, visited.GetVertices(algorithm));
            }
        }

        public void AddEndPoints(IAlgorithm algorithm, IIntermediateEndPoints endPoints)
        {
            source.Add(algorithm, endPoints.Source);
            target.Add(algorithm, endPoints.Target);
            intermediate.AddRange(algorithm, endPoints.IntermediateVertices);
        }

        public void AddPathVertices(IAlgorithm algorithm, IGraphPath grapPath)
        {
            path.AddRange(algorithm, grapPath.Path);
        }

        private readonly VisitedVertices visited;
        private readonly EnqueuedVertices enqueued;
        private readonly PathVertices path;
        private readonly IntermediateVertices intermediate;
        private readonly SourceVertices source;
        private readonly TargetVertices target;
        private readonly IVisualization visualizations;
        private readonly IProcessedVertices[] processedVertices;
    }
}