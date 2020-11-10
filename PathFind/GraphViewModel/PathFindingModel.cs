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

namespace GraphLib.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public int DelayTime { get; set; } // miliseconds

        public string AlgorithmKey { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            pathFindStatisticsFormat = ViewModelResources.StatisticsFormat;
            badResultMessage = ViewModelResources.BadResultMsg;
            DelayTime = 4;
        }

        public virtual void FindPath()
        {
            pathAlgorithm = AlgorithmFactory.CreateAlgorithm(AlgorithmKey, graph);

            PrepareAlgorithm();
            pathAlgorithm.FindPath();
            var path = pathAlgorithm.GetPath();

            mainViewModel.PathFindingStatistics +=
                string.Format("   " + pathFindStatisticsFormat,
                path.Count(),
                path.Sum(vertex => (int)vertex.Cost),
                graph.NumberOfVisitedVertices);
        }

        protected virtual void PrepareAlgorithm()
        {
            var timer = new Stopwatch();

            pathAlgorithm.OnVertexVisited += (vertex) =>
            {
                if (vertex.IsSimpleVertex())
                {
                    vertex.MarkAsVisited();
                }

                var timerCurrentInformation = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);
                var currentPathfindingStatistics = string.Format("   " + pathFindStatisticsFormat, 0, 0, graph.NumberOfVisitedVertices);
                mainViewModel.PathFindingStatistics = timerCurrentInformation + currentPathfindingStatistics;
            };

            pathAlgorithm.OnStarted += (sender, eventArgs) => { timer.Start(); };

            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                timer.Stop();
                mainViewModel.PathFindingStatistics = timer.GetTimeInformation(ViewModelResources.TimerInfoFormat);
                pathAlgorithm.GetPath().DrawPath();
            };

            pathAlgorithm.OnEnqueued += vertex =>
            {
                if (vertex.IsSimpleVertex())
                {
                    vertex.MarkAsEnqueued();
                }
            };
        }

        protected IPathFindingAlgorithm pathAlgorithm;
        protected IGraph graph;
        protected IMainModel mainViewModel;
        protected string badResultMessage;
        protected string pathFindStatisticsFormat;
    }
}
