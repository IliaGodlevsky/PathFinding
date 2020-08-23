using GraphLibrary.Algorithm;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.Graph;
using GraphLibrary.PathFindAlgorithmSelector;

namespace GraphLibrary.Model
{
    public abstract class AbstractPathFindModel : IModel
    {
        protected IPathFindAlgorithm pathFindAlgorythm;
        public Algorithms Algorithm { get; set; }

        protected AbstractGraph graph;
        protected IMainModel model;
        protected string badResultMessage;

        public AbstractPathFindModel(IMainModel model)
        {
            this.model = model;
            graph = model.Graph;
        }

        protected abstract void FindPreparations();

        public virtual void PathFind()
        {
            pathFindAlgorythm = AlgorithmSelector.GetPathFindAlgorithm(Algorithm, model.Graph);
            FindPreparations();
            if (pathFindAlgorythm.FindDestionation())
            {
                pathFindAlgorythm.DrawPath();
                model.Statistics = pathFindAlgorythm.StatCollector.GetStatistics().GetFormattedData();
                graph.Start = null;
                graph.End = null;
            }
            else
                ShowMessage(badResultMessage);
        }

        protected abstract void ShowMessage(string message);
    }
}
