using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Factory.Interface;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using Interruptable.EventArguments;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphViewModel
{
    public abstract class PathFindingModel
    {
        protected readonly BaseEndPoints endPoints;
        protected readonly Stopwatch timer;
        protected readonly ILog log;

        protected IGraphPath path;
        protected PathfindingAlgorithm algorithm;
        protected int visitedVerticesCount;

        public bool IsVisualizationRequired { get; set; } = true;

        public virtual int DelayTime { get; set; }

        public IAlgorithmFactory<PathfindingAlgorithm> Algorithm { get; set; }

        public IReadOnlyList<Tuple<string, IAlgorithmFactory<PathfindingAlgorithm>>> Algorithms { get; }

        protected PathFindingModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> factories, ILog log)
        {
            this.endPoints = endPoints;
            this.log = log;
            Algorithms = factories.GroupByGroupAttribute().ToNameInstanceTuples();
            timer = new Stopwatch();
            path = NullGraphPath.Instance;
        }

        public virtual async void FindPath()
        {
            try
            {
                algorithm = Algorithm.Create(endPoints);
                SubscribeOnAlgorithmEvents(algorithm);
                endPoints.RestoreCurrentColors();
                path = await algorithm.FindPathAsync();
                await path.HighlightAsync();
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
                path = NullGraphPath.Instance;
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current))
            {
                e.Current.AsVisualizable().VisualizeAsVisited();
            }
            if (!e.Current.IsNull())
            {
                visitedVerticesCount++;
            }
        }

        protected virtual void OnVertexVisitedNoVisualization(object sender, AlgorithmEventArgs e)
        {
            if (!e.Current.IsNull())
            {
                visitedVerticesCount++;
            }
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current))
            {
                e.Current.AsVisualizable().VisualizeAsEnqueued();
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

        protected virtual void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
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
            algorithm.Finished += timer.Stop;
            algorithm.Started += OnAlgorithmStarted;
            algorithm.Started += timer.Restart;
            algorithm.Interrupted += OnAlgorithmInterrupted;
            algorithm.Interrupted += timer.Stop;
            algorithm.Paused += timer.Stop;
            algorithm.Resumed += timer.Start;
        }
    }
}