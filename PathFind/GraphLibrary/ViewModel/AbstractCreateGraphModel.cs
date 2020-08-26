using GraphLibrary.Graph;
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

        public AbstractCreateGraphModel(IMainModel model)
        {
            this.model = model;
        }

        public abstract IGraphFactory GetFactory();

        public virtual void CreateGraph()
        {
            var factory = GetFactory();
            graph = factory.GetGraph();
            graphField = filler.FillGraphField(graph);
            model.Graph = graph;
            model.GraphField = graphField;
            model.GraphParametres = 
                GraphDataFormatter.GetFormattedData(model.Graph, model.Format);
            model.Statistics = string.Empty;
        }
    }
}
