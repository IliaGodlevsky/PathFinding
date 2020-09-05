using GraphFactory.GraphSaver;
using GraphLibrary.Collection;
using GraphLibrary.Common.Extensions;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphLoader;
using GraphLibrary.VertexEventHolder;

namespace GraphLibrary.Model
{
    public abstract class AbstractMainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }
        public virtual string Statistics { get; set; }
        public virtual IGraphField GraphField { get; set; }
        public virtual Graph Graph { get; set; }
        public AbstractVertexEventHolder VertexEventHolder { get; set; }
        public string GraphParametresFormat { get; protected set; }

        protected IGraphSaver saver;
        protected IGraphLoader loader;
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
            VertexEventHolder.Graph = Graph;
            VertexEventHolder.ChargeGraph();
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
        }

        public virtual void ClearGraph()
        {
            Graph.Refresh();
            Statistics = string.Empty;
            GraphParametres = Graph.GetFormattedInfo(GraphParametresFormat);
        }

        public abstract void FindPath();
        public abstract void CreateNewGraph();
    }
}
