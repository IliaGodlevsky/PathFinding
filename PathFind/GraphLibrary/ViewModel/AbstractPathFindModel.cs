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

        public AbstractPathFindModel(IMainModel model, 
            string badResultMessage, string format) : this(model)
        {
            this.format = format;
            this.badResultMessage = badResultMessage;
        }

        protected abstract void PrepareAlgorithm();

        public virtual void PathFind()
        {
            pathAlgorithm = AlgorithmSelector.
                GetPathFindAlgorithm(Algorithm, model.Graph);
            PrepareAlgorithm();
            pathAlgorithm.FindDestionation();
            if (pathAlgorithm.HasFoundPath())
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
