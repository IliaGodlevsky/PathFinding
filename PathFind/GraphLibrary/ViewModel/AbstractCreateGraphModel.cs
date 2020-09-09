using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphCreate.GraphFieldFiller;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;
using GraphLibrary.ViewModel.Interface;

namespace GraphLibrary.ViewModel
{
    public abstract class AbstractCreateGraphModel : IModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ObstaclePercent { get; set; }

        public AbstractCreateGraphModel(IMainModel model)
        {
            this.model = model;
        }

        public virtual void CreateGraph()
        {
            var factory = GetFactory();
            graph = factory.Graph;
            model.VertexEventHolder.Graph = graph;
            model.VertexEventHolder.ChargeGraph();
            graphField = graphFieldFiller.FileGraphField(graph);
            model.Graph = graph;
            model.GraphField = graphField;
            model.GraphParametres =
            model.Graph.GetFormattedInfo(model.GraphParametresFormat);
            model.Statistics = string.Empty;
        }

        public abstract IGraphFactory GetFactory();

        protected IMainModel model;
        protected Graph graph;
        protected IGraphField graphField;
        protected AbstractGraphFieldFiller graphFieldFiller;
    }
}
