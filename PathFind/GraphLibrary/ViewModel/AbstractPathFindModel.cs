using GraphLibrary.Algorithm;
using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Graph;
using GraphLibrary.AlgoSelector;

namespace GraphLibrary.Model
{
    public abstract class AbstractPathFindModel : IModel
    {
        protected IPathFindAlgorithm pathAlgorithm;
        public Algorithms Algorithm { get; set; }

        protected AbstractGraph graph;
        protected IMainModel model;
        protected string badResultMessage;
        protected string format;

        public AbstractPathFindModel(IMainModel model)
        {
            this.model = model;
            graph = model.Graph;
            format = LibraryResources.StatisticsFormat;
            badResultMessage = LibraryResources.BadResultMsg;
        }

        protected abstract void FindPreparations();

        public virtual void PathFind()
        {
            pathAlgorithm = AlgorithmSelector.
                GetPathFindAlgorithm(Algorithm, model.Graph);
            FindPreparations();
            if (pathAlgorithm.FindDestionation())
            {
                pathAlgorithm.DrawPath();
                model.Statistics = 
                    pathAlgorithm.StatCollector.
                    GetStatistics(format);
                graph.Start = null;
                graph.End = null;
            }
            else
                ShowMessage(badResultMessage);
        }

        protected abstract void ShowMessage(string message);
    }
}
