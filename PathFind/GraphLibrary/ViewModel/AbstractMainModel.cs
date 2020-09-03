using GraphFactory.GraphSaver;
using GraphLibrary.Collection;
using GraphLibrary.Common.Extensions;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphLoader;

namespace GraphLibrary.Model
{
    public abstract class AbstractMainModel : IMainModel
    {
        public virtual string GraphParametres { get; set; }
        public virtual string Statistics { get; set; }
        public virtual IGraphField GraphField { get; set; }
        public virtual Graph Graph { get; set; }
        public string Format { get; protected set; }

        protected IGraphSaver saver;
        protected IGraphLoader loader;
        protected AbstractVertexEventSetter vertexEventSetter;
        protected AbstractGraphFieldFiller graphFieldFiller;

        public AbstractMainModel()
        {
            
        }

        public AbstractMainModel(AbstractVertexEventSetter vertexEventSetter, 
            AbstractGraphFieldFiller graphFieldFiller, 
            IGraphSaver saver, IGraphLoader loader)
        {
            this.vertexEventSetter = vertexEventSetter;
            this.graphFieldFiller = graphFieldFiller;
            this.saver = saver;
            this.loader = loader;
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
            vertexEventSetter.ChargeGraph(Graph);
            GraphParametres = Graph.GetFormattedInfo(Format);
        }

        public virtual void ClearGraph()
        {
            vertexEventSetter.RefreshGraph(Graph);
            Statistics = string.Empty;
            GraphParametres = Graph.GetFormattedInfo(Format);
        }

        public abstract void FindPath();
        public abstract void CreateNewGraph();
    }
}
