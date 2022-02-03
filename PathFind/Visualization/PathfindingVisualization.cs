using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Infrastructure.Interfaces;
using GraphLib.Interfaces;
using System;
using Visualization.Extensions;
using Visualization.Interfaces;
using Visualization.Realizations;

namespace Visualization
{
    public abstract class PathfindingVisualization : IVisualization
    {
        protected PathfindingVisualization(IGraph graph)
        {
            visualizations = new CompositeVisualization(graph, costs, obstacles, enqueued, visited, path, intermediate, source, target);
            visualizationSlides = new CompositeVisualizationSlides(costs, obstacles, enqueued, visited, path, intermediate, source, target);
            this.graph = graph;
        }

        public void Clear() => visualizationSlides.Clear();

        public void Remove(IAlgorithm algorithm) => visualizationSlides.Remove(algorithm);

        public void Visualize(IAlgorithm algorithm) => visualizations.Visualize(algorithm);

        protected virtual void SubscribeOnCostChanged(INotifyVertexCostChanged notifier)
        {
            notifier.CostChanged += costs.OnStateChanged;
        }

        protected virtual void UnsubscribeFromCostChanged(INotifyVertexCostChanged notifier)
        {
            notifier.CostChanged -= costs.OnStateChanged;
        }

        protected virtual void SubscribeOnObstacleChanged(INotifyObstacleChanged notifier)
        {
            notifier.ObstacleChanged += obstacles.OnStateChanged;
        }

        protected virtual void UnsubscribeFromObstacleChanged(INotifyObstacleChanged notifier)
        {
            notifier.ObstacleChanged -= obstacles.OnStateChanged;
        }

        protected virtual void ReturnActualState()
        {
            costs.RestoreActualState();
            obstacles.RestoreActualState();
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

        protected void AddPathVertices(IAlgorithm algorithm, IGraphPath grapPath)
        {
            path.AddRange(algorithm, grapPath.Path);
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            if (sender is IAlgorithm algorithm)
            {
                obstacles.AddRange(algorithm, graph.Vertices);
                costs.AddRange(algorithm, graph.GetObstacles());
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

        private readonly VisitedVerticesSlides visited = new VisitedVerticesSlides();
        private readonly EnqueuedVerticesSlides enqueued = new EnqueuedVerticesSlides();
        private readonly PathVerticesSlides path = new PathVerticesSlides();
        private readonly IntermediateEndPointsSlides intermediate = new IntermediateEndPointsSlides();
        private readonly SourceVerticesSlides source = new SourceVerticesSlides();
        private readonly TargetVerticesSlides target = new TargetVerticesSlides();
        private readonly ObstacleSlides obstacles = new ObstacleSlides();
        private readonly CostSlides costs = new CostSlides();
        private readonly IVisualization visualizations;
        private readonly IVisualizationSlides visualizationSlides;
        private readonly IGraph graph;
    }
}