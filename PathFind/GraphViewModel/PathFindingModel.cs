using System.Diagnostics;
using System.Linq;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using Algorithm.AlgorithmCreating;
using Algorithm.PathFindingAlgorithms.Interface;
using GraphLib.Graphs.Abstractions;
using GraphLib.Extensions;
using Algorithm.Extensions;
using Common.Extensions;
using GraphLib.Vertex.Interface;
using System;
using Algorithm.EventArguments;
using GraphLib.PauseMaking;
using System.Collections.Generic;

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public int DelayTime { get; set; } // miliseconds

        public string AlgorithmKey { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {
            pathFindStatisticsFormat = ViewModelResources.StatisticsFormat;
            badResultMessage = ViewModelResources.BadResultMsg;
            pauseProvider = new PauseProvider();
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            DelayTime = 4;
        }

        public virtual void FindPath()
        {
            algorithm = AlgorithmFactory.
                CreateAlgorithm(AlgorithmKey, graph);

            algorithm.OnVertexEnqueued += OnVertexEnqueued;
            algorithm.OnVertexVisited += OnVertexVisited;
            algorithm.OnFinished += OnAlgorithmFinished;
            algorithm.OnStarted += OnAlgorithmStarted;
            algorithm.OnIteration += OnIteration;

            algorithm.FindPath();

            algorithm.OnVertexEnqueued -= OnVertexEnqueued;
            algorithm.OnVertexVisited -= OnVertexVisited;
            algorithm.OnFinished -= OnAlgorithmFinished;
            algorithm.OnStarted -= OnAlgorithmStarted;
            algorithm.OnIteration -= OnIteration;
        }

        protected virtual void OnVertexVisited(IVertex vertex)
        {
            if (vertex.IsSimpleVertex())
            {
                vertex.MarkAsVisited();
            }
        }

        protected virtual void OnVertexEnqueued(IVertex vertex)
        {
            if (vertex.IsSimpleVertex())
            {
                vertex.MarkAsEnqueued();
            }
        }

        protected virtual void OnIteration(object sender, EventArgs e)
        {
            mainViewModel.PathFindingStatistics 
                = GetUpdatedStatistics(new IVertex[] { });
                
            pauseProvider.Pause(DelayTime);
        }

        protected virtual void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            timer.Stop();
            var path = algorithm.GetPath();
            mainViewModel.PathFindingStatistics = GetUpdatedStatistics(path);

            if (e.HasFoundPath)
            { 
                path.DrawPath(); 
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            timer = new Stopwatch();
            timer.Start();
        }

        private string GetUpdatedStatistics(IEnumerable<IVertex> path)
        {
            string format = "   " + pathFindStatisticsFormat;
            var pathLength = path.Count();
            var pathCost = path.Sum(vertex => (int)vertex.Cost);
            var visited = graph.NumberOfVisitedVertices;
            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);
            var statistics = string.Format(format, pathLength, pathCost, visited);

            return timerInfo + statistics;
        }

        protected IPathFindingAlgorithm algorithm;
        protected string pathFindStatisticsFormat;
        protected PauseProvider pauseProvider;
        protected IMainModel mainViewModel;
        protected string badResultMessage;
        protected IGraph graph;

        private Stopwatch timer;
    }
}
