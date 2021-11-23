using Algorithm.Base;
using Algorithm.Extensions;
using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public bool IsVisualizationRequired { get; set; } = true;

        public int DelayTime { get; set; }

        public IAlgorithmFactory Algorithm { get; set; }

        public Tuple<string, IAlgorithmFactory>[] Algorithms { get; }

        protected PathFindingModel(BaseEndPoints endPoints,
            IEnumerable<IAlgorithmFactory> factories, ILog log)
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
                algorithm = Algorithm.CreateAlgorithm(endPoints);
                SubscribeOnAlgorithmEvents(algorithm);
                endPoints.ReturnColors();
                path = await algorithm.FindPathAsync();
                await path.HighlightAsync(endPoints);
                SummarizePathfindingResults();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                UnsubscribeOnAlgorithmEvents(algorithm);
                algorithm.Dispose();
                path = NullGraphPath.Instance;
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current))
            {
                (e.Current as IVisualizable)?.VisualizeAsVisited();
            }
            if (!e.Current.IsNull())
            {
                visitedVerticesCount++;
            }
        }

        protected virtual void OnVertexVisitedNoVisualization(object sender, EventArgs e)
        {
            visitedVerticesCount++;
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current))
            {
                (e.Current as IVisualizable)?.VisualizeAsEnqueued();
            }
        }

        protected virtual void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            timer.Stop();
        }

        protected virtual void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            timer.Stop();
        }

        protected abstract void SummarizePathfindingResults();

        protected virtual void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            visitedVerticesCount = 0;
            timer.Restart();
        }

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
            algorithm.Started += OnAlgorithmStarted;
            algorithm.Interrupted += OnAlgorithmInterrupted;
        }

        protected virtual void UnsubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            if (IsVisualizationRequired)
            {
                algorithm.VertexEnqueued -= OnVertexEnqueued;
                algorithm.VertexVisited -= OnVertexVisited;
            }
            else
            {
                algorithm.VertexVisited -= OnVertexVisitedNoVisualization;
            }
            algorithm.Finished -= OnAlgorithmFinished;
            algorithm.Started -= OnAlgorithmStarted;
            algorithm.Interrupted -= OnAlgorithmInterrupted;
        }

        protected readonly BaseEndPoints endPoints;
        protected readonly Stopwatch timer;
        protected readonly ILog log;

        protected IGraphPath path;
        protected PathfindingAlgorithm algorithm;
        protected int visitedVerticesCount;
    }
}