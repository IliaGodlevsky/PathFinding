using Algorithm.Algos;
using Algorithm.Algos.Enums;
using Algorithm.Common;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common;
using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using static GraphViewModel.Resources.ViewModelResources;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public bool IsVisualizationRequired { get; set; } = true;

        public int DelayTime { get; set; } = 4;

        public Algorithms Algorithm { get; set; }

        public IDictionary<string, Algorithms> Algorithms { get; }

        protected PathFindingModel(ILog log, 
            IMainModel mainViewModel, BaseEndPoints endPoints)
        {
            Algorithms = GetAlgorithmsDictinary();
            this.mainViewModel = mainViewModel;
            this.endPoints = endPoints;
            this.log = log;
            graph = mainViewModel.Graph;
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
        }

        public virtual async void FindPath()
        {
            try
            {
                algorithm = AlgoFactory
                    .CreateAlgorithm(Algorithm, graph, endPoints);
                SubscribeOnAlgorithmEvents();
                path = await algorithm.FindPathAsync();
                Summarize();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                algorithm.Dispose();
                endPoints.Reset();
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (!e.IsEndPoint && e.Vertex is IMarkable vertex)
            {
                vertex.MarkAsVisited();
            }
            visitedVerticesCount = e.VisitedVertices;
            mainViewModel.PathFindingStatistics = GetStatistics();
            interrupter.Suspend();
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (!e.IsEndPoint && e.Vertex is IMarkable vertex)
            {
                vertex.MarkAsEnqueued();
            }
        }

        protected virtual void OnAlgorithmInterrupted(object sender, InterruptEventArgs e)
        {
            log.Warn(AlgorithmInterruptedMsg);
            timer.Stop();
        }

        protected virtual void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            visitedVerticesCount = e.VisitedVertices;
            timer.Stop();
        }

        protected virtual void Summarize()
        {
            path.Highlight(endPoints);
            mainViewModel.PathFindingStatistics =
                path.PathLength > 0 ? GetStatistics() : PathWasNotFoundMsg;
        }

        protected virtual void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            interrupter = new Interrupter(DelayTime);
            timer.Reset();
            timer.Start();
        }

        private string GetStatistics()
        {
            string timerInfo = timer.Elapsed.ToString(@"mm\:ss\.ff");
            string algorithmName = ((Enum)Algorithm).GetDescription();
            string pathfindingInfo = string.Format(StatisticsFormat, PathfindingInfo);
            return string.Join("    ", algorithmName, timerInfo, pathfindingInfo);
        }

        private void SubscribeOnAlgorithmEvents()
        {
            if (IsVisualizationRequired)
            {
                algorithm.OnVertexEnqueued += OnVertexEnqueued;
                algorithm.OnVertexVisited += OnVertexVisited;
            }
            algorithm.OnFinished += OnAlgorithmFinished;
            algorithm.OnStarted += OnAlgorithmStarted;
            algorithm.OnInterrupted += OnAlgorithmInterrupted;
        }

        private IDictionary<string, Algorithms> GetAlgorithmsDictinary()
        {
            return Enum
                .GetValues(typeof(Algorithms))
                .Cast<Algorithms>()
                .ToDictionary(item => ((Enum)item).GetDescription())
                .OrderBy(item => item.Key)
                .AsDictionary();
        }

        private object[] PathfindingInfo 
            => new object[] { path.PathLength, path.PathCost, visitedVerticesCount };

        protected readonly BaseEndPoints endPoints;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected ISuspendable interrupter;
        protected IAlgorithm algorithm;
        protected IGraphPath path;
        private readonly IGraph graph;
        private readonly Stopwatch timer;
        private int visitedVerticesCount;
    }
}