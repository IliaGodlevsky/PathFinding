using GraphLibrary.Algorithm;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Collection;
using GraphLibrary.AlgoSelector;
using GraphLibrary.Common.Extensions;

namespace GraphLibrary.Model
{
    public abstract class AbstractPathFindModel : IModel
    {
        protected IPathFindAlgorithm pathAlgorithm;
        public Algorithms Algorithm { get; set; }

        protected Graph graph;
        protected IMainModel mainViewModel;
        protected string badResultMessage;
        protected string pathFindStatisticsFormat;

        public AbstractPathFindModel(IMainModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            graph = mainViewModel.Graph;
            pathFindStatisticsFormat = LibraryResources.StatisticsFormat;
            badResultMessage = LibraryResources.BadResultMsg;
        }

        protected abstract void PrepareAlgorithm();

        public virtual void FindPath()
        {
            pathAlgorithm = AlgorithmSelector.
                GetPathFindAlgorithm(Algorithm, mainViewModel.Graph);
            PrepareAlgorithm();
            if (pathAlgorithm.IsRightGraphSettings())
            {
                pathAlgorithm.FindDestionation();
                if (pathAlgorithm.HasFoundPathToEndVertex())
                {
                    pathAlgorithm.DrawPath();
                    mainViewModel.Statistics =
                        pathAlgorithm.StatCollector.
                        GetStatistics(pathFindStatisticsFormat);
                    graph.Start = null;
                    graph.End = null;
                }
                else
                    ShowMessage(badResultMessage);
            }
        }

        protected abstract void ShowMessage(string message);
    }
}
