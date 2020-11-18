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

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public int DelayTime { get; set; } // milliseconds

        public string AlgorithmKey { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {
            badResultMessage = ViewModelResources.BadResultMsg;
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

            var visitedVertices = graph.NumberOfVisitedVertices;
            mainViewModel.PathFindingStatistics = GetIntermediateStatistics(
                timer, 
                steps: 0, 
                pathLength: 0, 
                visitedVertices);

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

            var path = new Path();
            var isPathExtracted = path.ExtractPath(graph);

            mainViewModel.PathFindingStatistics
                = GetIntermediateStatistics(
                    timer, 
                    path.PathCost, 
                    path.PathLength, 
                    graph.NumberOfVisitedVertices);

            if (isPathExtracted)
            {
                path.HighlightPath();
            }
        }

        protected virtual void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            timer = new Stopwatch();
            timer.Start();
        }

        private string GetIntermediateStatistics(
            Stopwatch timer, 
            int steps, 
            int pathLength, 
            int visitedVertices)
        {
            var graphInfo = string.Format(
                ViewModelResources.StatisticsFormat,
                steps, 
                pathLength, 
                visitedVertices);
            var timerInfo = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);
            return timerInfo + "   " + graphInfo;
        }

        protected PauseProvider pauseProvider;
        protected IMainModel mainViewModel;
        protected string badResultMessage;
        protected IGraph graph;

        private Stopwatch timer;
    }
}
