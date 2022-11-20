using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using Shared.Process.EventArguments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.Visualization.Models
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;

    public abstract class PathFindingModel<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected readonly PathfindingRangeAdapter<TVertex> pathfindingRange;
        protected readonly Stopwatch timer;
        protected readonly ILog log;
        protected IPathfindingRange range;

        protected PathfindingProcess algorithm;

        protected int visitedVerticesCount;

        protected IGraphPath Path { get; set; } = NullGraphPath.Instance;

        protected IGraph<TVertex> Graph { get; }

        public bool IsVisualizationRequired { get; set; } = true;

        public virtual TimeSpan Delay { get; set; }

        public IAlgorithmFactory<PathfindingProcess> Algorithm { get; set; }

        public IReadOnlyList<IAlgorithmFactory<PathfindingProcess>> Algorithms { get; }

        protected PathFindingModel(PathfindingRangeAdapter<TVertex> endPoints,
            IEnumerable<IAlgorithmFactory<PathfindingProcess>> factories, IGraph<TVertex> graph, ILog log)
        {
            Graph = graph;
            this.pathfindingRange = endPoints;
            this.log = log;
            Algorithms = factories
                .GroupBy(item => item.GetAttributeOrDefault<GroupAttribute>())
                .SelectMany(item => item.OrderByOrderAttribute<AlgorithmFactory, OrderAttribute>())
                .ToList();
            timer = new Stopwatch();
            Path = NullGraphPath.Interface;
        }

        public virtual async void FindPath()
        {
            try
            {
                range = pathfindingRange.GetPathfindingRange();
                algorithm = Algorithm.Create(range);
                SubscribeOnAlgorithmEvents(algorithm);
                pathfindingRange.RestoreVerticesVisualState();
                Path = await algorithm.FindPathAsync();
                await Path.Select(Graph.Get).Reverse().VisualizeAsPathAsync();
                SummarizePathfindingResults();
            }
            catch (PathfindingException ex)
            {
                log.Debug(ex.Message);
                SummarizePathfindingResults();
            }
            catch (Exception ex)
            {
                algorithm.Interrupt();
                log.Error(ex);
            }
            finally
            {
                algorithm.Dispose();
            }
        }

        protected virtual void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            var current = Graph.Get(e.Current);
            if (!range.IsInRange(current))
            {
                current.VisualizeAsVisited();
            }
            visitedVerticesCount++;
        }

        protected virtual void OnVertexVisitedNoVisualization(object sender, PathfindingEventArgs e)
        {
            visitedVerticesCount++;
        }

        protected virtual void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            var current = Graph.Get(e.Current);
            if (!range.IsInRange(current))
            {
                current.VisualizeAsEnqueued();
            }
        }

        protected virtual void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
        }

        protected virtual void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
        }

        protected virtual void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
        }

        protected abstract void SummarizePathfindingResults();

        protected virtual void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
            if (IsVisualizationRequired)
            {
                algorithm.VertexEnqueued += OnVertexEnqueued;
                algorithm.VertexVisited += OnVertexVisited;
            }
            else
            {
                algorithm.VertexVisited += OnVertexVisitedNoVisualization;
            }
            algorithm.Finished += OnAlgorithmFinished;
            algorithm.Finished += (s, e) => timer.Stop();
            algorithm.Started += OnAlgorithmStarted;
            algorithm.Started += (s, e) => timer.Restart();
            algorithm.Interrupted += OnAlgorithmInterrupted;
            algorithm.Interrupted += (s, e) => timer.Stop();
            algorithm.Paused += (s, e) => timer.Stop();
            algorithm.Resumed += (s, e) => timer.Start();
        }
    }
}