using Algorithm.Algos.Enums;
using Algorithm.Algos.Extensions;
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using EnumerationValues.Extensions;
using EnumerationValues.Realizations;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
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

        public Tuple<string, Algorithms>[] Algorithms => algorithms.Value;

        protected PathFindingModel(ILog log, IMainModel mainViewModel,
            BaseEndPoints endPoints)
        {
            this.mainViewModel = mainViewModel;
            this.endPoints = endPoints;
            this.log = log;
            var withoutIgnoredEnumValues = new EnumValuesWithoutIgnored<Algorithms>(new EnumValues<Algorithms>());
            algorithms = new Lazy<Tuple<string, Algorithms>[]>
                (withoutIgnoredEnumValues.ToAdjustedAndOrderedByDescriptionTuples);
            graph = mainViewModel.Graph;
            timer = new Stopwatch();
            path = new NullGraphPath();
            algorithm = new NullAlgorithm();
        }

        public virtual async void FindPath()
        {
            try
            {
                algorithm = Algorithm.ToAlgorithm(graph, endPoints);
                SubscribeOnAlgorithmEvents();
                path = await algorithm.FindPathAndHighlightAsync(endPoints);
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
            if (CanBeVisualized(e.Current))
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
            if (CanBeVisualized(e.Current))
            {
                (e.Current as IVisualizable)?.VisualizeAsEnqueued();
            }
        }

        protected virtual void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            log.Warn(AlgorithmInterruptedMsg);
            timer.Stop();
        }

        protected virtual void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            timer.Stop();
        }

        protected abstract void Summarize();

        protected virtual void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            timer.Restart();
        }

        protected string GetStatistics()
        {
            string timerInfo = timer.Elapsed.ToString(@"mm\:ss\.ff");
            var description = Algorithms.FirstOrDefault(item => item.Item2 == Algorithm).Item1;
            string pathfindingInfo = string.Format(StatisticsFormat, PathfindingInfo);
            return string.Join("\t", description, timerInfo, pathfindingInfo);
        }

        protected string CouldntFindPathMsg => PathWasNotFoundMsg;

        private void SubscribeOnAlgorithmEvents()
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

        private bool CanBeVisualized(IVertex vertex)
        {
            return !endPoints.IsEndPoint(vertex)
                && vertex is IVisualizable;
        }

        private object[] PathfindingInfo
            => new object[] { path.PathLength, path.PathCost, visitedVerticesCount };

        protected readonly BaseEndPoints endPoints;
        protected readonly IMainModel mainViewModel;
        protected readonly ILog log;
        protected IAlgorithm algorithm;
        protected IGraphPath path;
        private readonly IGraph graph;
        private readonly Lazy<Tuple<string, Algorithms>[]> algorithms;
        protected readonly Stopwatch timer;
        protected int visitedVerticesCount;
    }
}