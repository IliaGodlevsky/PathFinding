using System.Diagnostics;
using GraphViewModel.Interfaces;
using GraphViewModel.Resources;
using Algorithm.AlgorithmCreating;
using GraphLib.Graphs.Abstractions;
using GraphLib.Extensions;
using Common.Extensions;
using GraphLib.Vertex.Interface;
using Algorithm.EventArguments;
using GraphLib.PauseMaking;
using System;
using Common.EventArguments;
using GraphLib.Graphs.Infrastructure;
using GraphLib.Graphs.EventArguments;

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public event EventHandler OnPathNotFound;

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

        protected virtual void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            timer.Stop();

            var path = new GraphPath(graph);
            path.OnVertexHighlighted += OnVertexHighlighted;


            mainViewModel.PathFindingStatistics = GetIntermediateStatistics(timer, 
                path.PathLength, path.PathCost, graph.NumberOfVisitedVertices);

            if (path.IsExtracted)
            {
                path.HighlightPath();
            }
            else
            {
                var args = new PathNotFoundEventArgs("Couln't find path");
                OnPathNotFound?.Invoke(this, args);
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            timer = new Stopwatch();
            timer.Start();
        }

        protected virtual void OnVertexHighlighted(object sender, GraphPathEventArgs e)
        {
            if (e.Vertex.IsSimpleVertex())
            {
                e.Vertex.MarkAsPath();
            }
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
