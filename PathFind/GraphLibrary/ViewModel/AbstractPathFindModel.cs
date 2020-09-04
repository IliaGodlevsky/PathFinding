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
        public Algorithms Algorithm { get; set; }

        public AbstractPathFindModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            pathFindStatisticsFormat = LibraryResources.StatisticsFormat;
            badResultMessage = LibraryResources.BadResultMsg;
        }

        public virtual void FindPath()
        {
            pathAlgorithm = AlgorithmSelector.GetPathFindAlgorithm(Algorithm, mainViewModel.Graph);
            PrepareAlgorithm();           
            if (pathAlgorithm.IsRightGraphSettings())
            {
                var timer = new Stopwatch();
                timer.Start();
                pathAlgorithm.FindDestionation();
                timer.Stop();
                if (pathAlgorithm.HasFoundPathToEndVertex())
                {
                    pathAlgorithm.DrawPath();
                    var collector = new StatisticsCollector();
                    collector.CollectStatistics(pathAlgorithm.Graph, timer);
                    mainViewModel.Statistics = collector.GetStatistics(pathFindStatisticsFormat);
                    graph.Start = null;
                    graph.End = null;
                }
                else
                    ShowMessage(badResultMessage);
            }
        }

        protected abstract void ShowMessage(string message);

        protected IPathFindAlgorithm pathAlgorithm;

        protected abstract void PrepareAlgorithm();
        protected Graph graph;
        protected IMainModel mainViewModel;
        protected string badResultMessage;
        protected string pathFindStatisticsFormat;
    }
}
