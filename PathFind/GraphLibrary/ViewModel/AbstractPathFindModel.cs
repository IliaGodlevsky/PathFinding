using GraphLibrary.Algorithm;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Collection;
using GraphLibrary.AlgoSelector;
using GraphLibrary.Common.Extensions;
using GraphLibrary.Statistics;
using System.Diagnostics;

namespace GraphLibrary.Model
{
    public abstract class AbstractPathFindModel : IModel
    {   
        public int DelayTime { get; set; } // miliseconds
        public Algorithms Algorithm { get; set; }

        public AbstractPathFindModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            pathFindStatisticsFormat = LibraryResources.StatisticsFormat;
            badResultMessage = LibraryResources.BadResultMsg;
            DelayTime = 9;
            timer = new Stopwatch();
        }

        private void SubsribeAlgorithm()
        {
            pathAlgorithm.OnVertexVisited += (vertex) => { pathAlgorithm.VisitVertex(vertex); };
            pathAlgorithm.OnAlgorithmStarted += () => { timer.Start(); };
            pathAlgorithm.OnAlgorithmFinished += () =>
            {
                timer.Stop();
                if (pathAlgorithm.HasFoundPathToEndVertex())
                {
                    pathAlgorithm.DrawPath();
                    mainViewModel.Statistics = mainViewModel.Graph.
                        GetFormattedStatistics(timer, pathFindStatisticsFormat);
                }
            };
        }

        public virtual void FindPath()
        {
            pathAlgorithm = AlgorithmSelector.GetPathFindAlgorithm(Algorithm, graph);
            PrepareAlgorithm();
            SubsribeAlgorithm();
            pathAlgorithm.FindDestionation();
        }

        protected IPathFindAlgorithm pathAlgorithm;

        protected abstract void PrepareAlgorithm();

        private readonly Stopwatch timer;
        protected Graph graph;
        protected IMainModel mainViewModel;
        protected string badResultMessage;
        protected string pathFindStatisticsFormat;
    }
}
