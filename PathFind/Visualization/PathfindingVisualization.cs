using Algorithm.Base;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Commands.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;
using Visualization.Extensions;
using Visualization.Interfaces;
using Visualization.Realizations;

namespace Visualization
{
    public abstract class PathfindingVisualization : IExecutable<IAlgorithm<IGraphPath>>, IVisualizationSlides<IGraphPath>, IVisualizationSlides<IPathfindingRange>
    {
        private readonly VisitedVertices visited = new VisitedVertices();
        private readonly EnqueuedVertices enqueued = new EnqueuedVertices();
        private readonly PathVertices path = new PathVertices();
        private readonly IntermediateVertices intermediate = new IntermediateVertices();
        private readonly SourceVertices source = new SourceVertices();
        private readonly TargetVertices target = new TargetVertices();
        private readonly ObstacleVertices obstacles = new ObstacleVertices();
        private readonly IExecutable<IAlgorithm<IGraphPath>> visualizations;
        private readonly IVisualizationSlides<IVertex> visualizationSlides;
        private readonly IGraph graph;

        protected PathfindingVisualization(IGraph graph)
        {
            visualizations = new CompositeVisualization(graph) { obstacles, enqueued, visited, path, intermediate, source, target };
            visualizationSlides = new CompositeVisualizationSlides { obstacles, enqueued, visited, path, intermediate, source, target };
            this.graph = graph;
        }

        public void Clear()
        {
            visualizationSlides.Clear();
        }

        public void Remove(IAlgorithm<IGraphPath> algorithm)
        {
            visualizationSlides.Remove(algorithm);
        }

        public void Execute(IAlgorithm<IGraphPath> algorithm)
        {
            visualizations.Execute(algorithm);
        }

        protected virtual void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.VertexEnqueued += OnVertexEnqueued;
            algorithm.Finished += OnAlgorithmFinished;
            algorithm.Started += OnAlgorithmStarted;
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, IPathfindingRange endPoints)
        {
            source.Add(algorithm, endPoints.Source);
            target.Add(algorithm, endPoints.Target);
            var intermediates = endPoints.GetIntermediates();
            intermediate.AddRange(algorithm, intermediates);
        }

        public void Add(IAlgorithm<IGraphPath> algorithm, IGraphPath graphPath)
        {
            var endPoints = source.GetVertices(algorithm)
                .Concat(target.GetVertices(algorithm))
                .Concat(intermediate.GetVertices(algorithm));
            var vertices = graphPath.Where(item => !endPoints.Contains(item));
            path.AddRange(algorithm, vertices);
        }

        protected virtual void OnAlgorithmStarted(object sender, EventArgs e)
        {
            if (sender is IAlgorithm<IGraphPath> algorithm)
            {
                obstacles.AddRange(algorithm, graph.GetObstacles());
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (sender is IAlgorithm<IGraphPath> algo && e.Current.TryVisualizeAsVisited())
            {
                visited.Add(algo, e.Current);
            }
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (sender is IAlgorithm<IGraphPath> algo && e.Current.TryVisualizeAsEnqueued())
            {
                enqueued.Add(algo, e.Current);
            }
        }

        protected virtual void OnAlgorithmFinished(object sender, EventArgs e)
        {
            if (sender is IAlgorithm<IGraphPath> algorithm)
            {
                enqueued.RemoveRange(algorithm, visited.GetVertices(algorithm));
            }
        }
    }
}