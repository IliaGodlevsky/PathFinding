using Algorithm.Algos;
using Algorithm.Algos.Enums;
using Algorithm.Common;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static GraphViewModel.Resources.ViewModelResources;

namespace GraphViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public bool IsVisualizationRequired { get; set; } = true;

        public int DelayTime { get; set; } = 4;

        public Algorithms Algorithm { get; set; }

        public IDictionary<string, Algorithms> Algorithms { get; }

        protected PathFindingModel(ILog log, IMainModel mainViewModel,
            BaseEndPoints endPoints)
        {
            this.mainViewModel = mainViewModel;
            this.endPoints = endPoints;
            this.log = log;
            Algorithms = GetAlgorithmsDictinary();
            graph = mainViewModel.Graph;
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
        }

        public virtual async void FindPath()
        {
            try
            {
                algorithm = AlgoFactory.CreateAlgorithm(Algorithm, graph, endPoints);
                SubscribeOnAlgorithmEvents();
                path = await algorithm.FindPathAsync();
                await path.HighlightAsync(endPoints);
                Summarize();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                algorithm.Dispose();
            }
        }

        protected virtual void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current) && e.Current is IMarkable vertex)
            {
                vertex.MarkAsVisited();
            }
            visitedVerticesCount++;
        }

        protected virtual void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (!endPoints.IsEndPoint(e.Current) && e.Current is IMarkable vertex)
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
            timer.Stop();
        }

        protected abstract void Summarize();

        protected virtual void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            timer.Reset();
            timer.Start();
        }

        protected string GetStatistics()
        {
            string timerInfo = timer.Elapsed.ToString(@"mm\:ss\.ff");
            string algorithmName = ((Enum)Algorithm).GetDescription();
            string pathfindingInfo = string.Format(StatisticsFormat, PathfindingInfo);
            return string.Join("\t", algorithmName, timerInfo, pathfindingInfo);
        }

        protected string CouldntFindPathMsg => PathWasNotFoundMsg;

        private void SubscribeOnAlgorithmEvents()
        {
            if (IsVisualizationRequired)
            {
                algorithm.VertexEnqueued += OnVertexEnqueued;
                algorithm.VertexVisited += OnVertexVisited;
            }
            algorithm.Finished += OnAlgorithmFinished;
            algorithm.Started += OnAlgorithmStarted;
            algorithm.Interrupted += OnAlgorithmInterrupted;
        }

        private IDictionary<string, Algorithms> GetAlgorithmsDictinary()
        {
            string Description(Algorithms item) => ((Enum)item).GetDescription();
            return EnumExtensions.ToOrderedDictionary<string, Algorithms>(Description, item => item.Key);
        }

        private object[] PathfindingInfo
            => new object[] { path.PathLength, path.PathCost, visitedVerticesCount };

        protected readonly BaseEndPoints endPoints;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected IAlgorithm algorithm;
        protected IGraphPath path;
        private readonly IGraph graph;
        protected readonly Stopwatch timer;
        private int visitedVerticesCount;
    }
}