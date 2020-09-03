using GraphLibrary.Graph;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphFactory;

namespace GraphLibrary.Model
{
    public abstract class AbstractCreateGraphModel : IModel
    {
        public string Width { get; set; }
        public string Height { get; set; }
        public int ObstaclePercent { get; set; }

        protected IMainModel model;
        protected AbstractGraph graph;
        protected IGraphField graphField;
        protected AbstractGraphFiller filler;
        protected AbstractGraphFieldFiller graphFieldFiller;

        public AbstractCreateGraphModel(IMainModel model)
        {
            this.model = model;
        }

        public abstract IGraphFactory GetFactory();

        public virtual void CreateGraph()
        {
            var factory = GetFactory();
            graph = factory.GetGraph();
            filler.ChargeGraph(graph);
            graphField = graphFieldFiller.FileGraphField(graph);
            model.Graph = graph;
            model.GraphField = graphField;
            model.GraphParametres = 
                GraphParametresPresenter.GetFormattedData(model.Graph, model.Format);
            model.Statistics = string.Empty;
        }
    }
}
