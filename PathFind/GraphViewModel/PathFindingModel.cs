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
        public bool IsProcessDisplayingRequired { get; set; }

        public int DelayTime { get; set; }

        public Algorithms Algorithm { get; set; }

        public IDictionary<string, Algorithms> Algorithms => algorithms.Value;

        protected PathFindingModel(ILog log, IMainModel mainViewModel, BaseEndPoints endPoints)
        {
            algorithms = new Lazy<IDictionary<string, Algorithms>>(GetAlgorithmsDictinary);
            this.mainViewModel = mainViewModel;
            this.endPoints = endPoints;
            this.log = log;
            DelayTime = 4;
            graph = mainViewModel.Graph;
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
            IsProcessDisplayingRequired = true;
        }

        public virtual async void FindPath()
        {
            try
            {
                algorithm = AlgoFactory.CreateAlgorithm(Algorithm, graph, endPoints);
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

        protected readonly BaseEndPoints endPoints;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected ISuspendable interrupter;
        protected IAlgorithm algorithm;
        protected IGraphPath path;

        private string GetStatistics()
        {
            string timerInfo = timer.Elapsed.ToString(@"mm\:ss\.ff");
            string graphInfo = string.Format(StatisticsFormat, path.PathLength, path.PathCost, visitedVerticesCount);
            return string.Join("    ", ((Enum)Algorithm).GetDescription(), timerInfo, graphInfo);
        }

        private void SubscribeOnAlgorithmEvents()
        {
            if (IsProcessDisplayingRequired)
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

        private readonly Lazy<IDictionary<string, Algorithms>> algorithms;
        private readonly IGraph graph;
        private readonly Stopwatch timer;
        private int visitedVerticesCount;
    }
}