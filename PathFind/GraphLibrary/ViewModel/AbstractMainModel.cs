using GraphFactory.GraphSaver;
using GraphLibrary.Graph;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphLoader;

namespace GraphLibrary.Model
{
    public abstract class AbstractMainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }
        public virtual string Statistics { get; set; }
        public virtual IGraphField GraphField { get; set; }
        public virtual AbstractGraph Graph { get; set; }
        public string Format { get; protected set; }

        protected IGraphSaver saver;
        protected IGraphLoader loader;
        protected AbstractGraphFiller filler;
        protected AbstractGraphFieldFiller graphFieldFiller;

        public AbstractMainModel()
        {
            
        }

        public virtual void SaveGraph()
        {
            saver.SaveGraph(Graph);
        }

        public virtual void LoadGraph()
        {
            var temp = loader.GetGraph();
            if (temp == null)
                return;
            Graph = temp;
            GraphField = graphFieldFiller.FileGraphField(Graph);
            filler.ChargeGraph(Graph);
            GraphParametres = GraphParametresPresenter.GetFormattedData(Graph, Format);
        }

        public virtual void ClearGraph()
        {
            filler.RefreshGraph(Graph);
            Statistics = string.Empty;
            GraphParametres = GraphParametresPresenter.GetFormattedData(Graph, Format);
        }

        public abstract void PathFind();
        public abstract void CreateNewGraph();
    }
}
