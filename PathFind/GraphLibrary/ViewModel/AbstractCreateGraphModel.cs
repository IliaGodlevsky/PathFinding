using GraphLibrary.Collection;
using GraphLibrary.Common.Extensions;
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
        protected Graph graph;
        protected IGraphField graphField;
        protected AbstractVertexEventSetter vertexEventSetter;
        protected AbstractGraphFieldFiller graphFieldFiller;

        public AbstractCreateGraphModel(IMainModel model)
        {
            this.model = model;
        }
        public AbstractCreateGraphModel(IMainModel model, 
            AbstractGraphFieldFiller graphFieldFiller, 
            AbstractVertexEventSetter eventSetter) : this(model)
        {
            this.graphFieldFiller = graphFieldFiller;
            vertexEventSetter = eventSetter;
        }

        public abstract IGraphFactory GetFactory();

        public virtual void CreateGraph()
        {
            var factory = GetFactory();
            graph = factory.GetGraph();
            vertexEventSetter.ChargeGraph(graph);
            graphField = graphFieldFiller.FileGraphField(graph);
            model.Graph = graph;
            model.GraphField = graphField;
            model.GraphParametres =
                model.Graph.GetFormattedInfo(model.Format);
            model.Statistics = string.Empty;
        }
    }
}
