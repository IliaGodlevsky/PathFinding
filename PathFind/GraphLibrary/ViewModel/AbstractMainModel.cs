using GraphLibrary.EventHolder;
using GraphLibrary.Extensions;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;
using GraphLibrary.GraphSerialization.GraphLoader.Interface;
using GraphLibrary.GraphSerialization.GraphSaver.Interface;
using GraphLibrary.ViewModel.Interface;

namespace GraphLibrary.ViewModel
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
            saver.SaveGraph(Graph, GetSavePath());
        }

        public virtual void LoadGraph()
        {
            var temp = loader.GetGraph(GetLoadPath());
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

        protected abstract string GetSavePath();
        protected abstract string GetLoadPath();
        public abstract void FindPath();
        public abstract void CreateNewGraph();
    }
}
