using Algorithm.Extensions;
using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public bool IsVisualizationRequired { get; set; } = true;

        public int DelayTime { get; set; } = 5;

        public IAlgorithmFactory Algorithm { get; set; }

        public Tuple<string, IAlgorithmFactory>[] Algorithms { get; }

        protected PathFindingModel(ILog log, IGraph graph, BaseEndPoints endPoints, 
            IEnumerable<IAlgorithmFactory> factories)
        {
            this.endPoints = endPoints;
            this.log = log;
            this.graph = graph;
            Algorithms = factories.ToOrderedNameInstanceTuples(item => item.Item1);
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
        }

        public virtual async void FindPath()
        {
            try
            {
                algorithm = Algorithm.CreateAlgorithm(graph, endPoints);
                SubscribeOnAlgorithmEvents(algorithm);
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
                algorithm.Dispose();
                path = new NullGraphPath();
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current))
            {
                (e.Current as IVisualizable)?.VisualizeAsVisited();
            }
            visitedVerticesCount++;
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

        protected virtual void SubscribeOnAlgorithmEvents(IAlgorithm algorithm)
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

        protected readonly BaseEndPoints endPoints;
        protected readonly IGraph graph;
        protected readonly ILog log;
        protected readonly Stopwatch timer;

        protected IGraphPath path;
        protected IAlgorithm algorithm;
        protected int visitedVerticesCount;
    }
}