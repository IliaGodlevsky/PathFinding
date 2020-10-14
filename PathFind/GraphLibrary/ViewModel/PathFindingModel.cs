using System.Diagnostics;
using System.Linq;
using GraphLibrary.PathFindingAlgorithm.Interface;
using GraphLibrary.ViewModel.Interface;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.AlgorithmCreating;

namespace GraphLibrary.ViewModel
{
    public abstract class PathFindingModel : IModel
    {
        public int DelayTime { get; set; } // miliseconds
        public string AlgorithmKey { get; set; }

        public PathFindingModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            pathFindStatisticsFormat = LibraryResources.StatisticsFormat;
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
                path.Sum(vertex => vertex.Cost),
                graph.NumberOfVisitedVertices);
        }

        protected virtual void PrepareAlgorithm()
        {
            var timer = new Stopwatch();
            pathAlgorithm.OnVertexVisited += (vertex) =>
            {
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsVisited();
                mainViewModel.PathFindingStatistics = timer.GetTimeInformation(LibraryResources.TimerInfoFormat) +
                string.Format("   " + pathFindStatisticsFormat, 0, 0, graph.NumberOfVisitedVertices);
            };
            pathAlgorithm.OnStarted += (sender, eventArgs) => { timer.Start(); };
            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                timer.Stop();
                mainViewModel.PathFindingStatistics = timer.GetTimeInformation(LibraryResources.TimerInfoFormat);
                pathAlgorithm.GetPath().DrawPath();
            };
            pathAlgorithm.OnEnqueued += vertex =>
            {
                if (vertex.IsSimpleVertex())
                    vertex.MarkAsEnqueued();
            };
        }

        protected IPathFindingAlgorithm pathAlgorithm;
        protected IGraph graph;
        protected IMainModel mainViewModel;
        protected string pathFindStatisticsFormat;
    }
}
