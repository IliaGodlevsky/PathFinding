using Algorithm.AlgorithmCreating;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Infrastructure;
using GraphLib.PauseMaking;
using GraphLib.Vertex.Interface;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using System;
using System.Diagnostics;

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event Action<string> OnPathNotFound;

        public int DelayTime { get; set; } // milliseconds

        public string AlgorithmKey { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {
            pauseProvider = new PauseProvider();
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            DelayTime = 4;
        }

        public virtual void FindPath()
        {
            var algorithm = AlgorithmFactory.
                CreateAlgorithm(AlgorithmKey, graph);

            algorithm.OnVertexEnqueued += OnVertexEnqueued;
            algorithm.OnVertexVisited += OnVertexVisited;
            algorithm.OnFinished += OnAlgorithmFinished;
            algorithm.OnStarted += OnAlgorithmStarted;

            algorithm.FindPath();
        }

        protected virtual void OnVertexVisited(IVertex vertex)
        {
            if (vertex.IsSimpleVertex())
            {
                vertex.MarkAsVisited();
            }

            mainViewModel.PathFindingStatistics = GetIntermediateStatistics(timer,
                steps: 0, pathLength: 0, graph.NumberOfVisitedVertices);

            pauseProvider.Pause(DelayTime);
        }

        protected virtual void OnVertexEnqueued(IVertex vertex)
        {
            if (vertex.IsSimpleVertex())
            {
                vertex.MarkAsEnqueued();
            }
        }

        protected virtual void OnAlgorithmFinished()
        {
            timer.Stop();

            var path = new GraphPath(graph);


            mainViewModel.PathFindingStatistics = GetIntermediateStatistics(timer,
                path.PathLength, path.PathCost, graph.NumberOfVisitedVertices);

            if (path.IsExtracted)
            {
                path.HighlightPath();
            }
            else
            {
                OnPathNotFound?.Invoke("Couln't find path");
            }
        }

        protected virtual void OnAlgorithmStarted()
        {
            timer = new Stopwatch();
            timer.Start();
        }

        private string GetIntermediateStatistics(Stopwatch timer,
            int steps, int pathLength, int visitedVertices)
        {
            var graphInfo = string.Format(ViewModelResources.StatisticsFormat,
                steps, pathLength, visitedVertices);

            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);

            return AlgorithmKey + "   " + timerInfo + "   " + graphInfo;
        }

        protected PauseProvider pauseProvider;
        protected IMainModel mainViewModel;
        protected IGraph graph;

        private Stopwatch timer;
    }
}
