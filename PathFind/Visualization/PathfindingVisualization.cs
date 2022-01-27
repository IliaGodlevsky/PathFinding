using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Attrbiutes;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Visualization.Extensions;
using Visualization.Interfaces;
using Visualization.Realizations;

namespace Visualization
{
    public abstract class PathfindingVisualization : IVisualization
    {
        protected PathfindingVisualization(IGraph graph)
        {
            var instances = this.InitializeRequiredFields();
            visualizations = new CompositeVisualization(graph, instances.OfType<IVisualization>().ToArray());
            visualizationSlides = instances.OfType<IVisualizationSlides>().ToArray();
            this.graph = graph;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            visualizationSlides.ForEach(slides => slides.Clear());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(IAlgorithm algorithm)
        {
            visualizationSlides.ForEach(slides => slides.Remove(algorithm));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Visualize(IAlgorithm algorithm)
        {
            visualizations.Visualize(algorithm);
        }

        protected virtual void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.Finished += OnAlgorithmFinished;
            algorithm.Started += OnAlgorithmStarted;
        }

        protected void AddEndPoints(IAlgorithm algorithm, IEndPoints endPoints)
        {
            source.Add(algorithm, endPoints.Source);
            target.Add(algorithm, endPoints.Target);
            var intermediates = endPoints.GetIntermediates();
            intermediate.AddRange(algorithm, intermediates);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void AddPathVertices(IAlgorithm algorithm, IGraphPath grapPath)
        {
            path.AddRange(algorithm, grapPath.Path);
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            if (sender is IAlgorithm algorithm)
            {
                obstacles.AddRange(algorithm, graph.GetObstacles());
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (sender is IAlgorithm algo && e.Current.TryVisualizeAsVisited())
            {
                visited.Add(algo, e.Current);
            }
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (sender is IAlgorithm algo && e.Current.TryVisualizeAsEnqueued())
            {
                enqueued.Add(algo, e.Current);
            }
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (sender is IAlgorithm algorithm)
            {
                enqueued.RemoveRange(algorithm, visited.GetVertices(algorithm));
            }
        }
#pragma warning disable 0649
        [InitializationRequired(2)] private readonly VisitedVertices visited;
        [InitializationRequired(1)] private readonly EnqueuedVertices enqueued;
        [InitializationRequired(3)] private readonly PathVertices path;
        [InitializationRequired(4)] private readonly IntermediateVertices intermediate;
        [InitializationRequired(5)] private readonly SourceVertices source;
        [InitializationRequired(6)] private readonly TargetVertices target;
        [InitializationRequired(0)] private readonly ObstacleVertices obstacles;
#pragma warning restore 0649
        private readonly IVisualization visualizations;
        private readonly IVisualizationSlides[] visualizationSlides;
        private readonly IGraph graph;
    }
}