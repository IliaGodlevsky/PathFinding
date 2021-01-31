using Common.ValueRanges;
using GraphLib.Graphs.Factories.Interfaces;
using GraphViewModel.Interfaces;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public ValueRange GraphWidthValueRange { get; protected set; }

        public ValueRange GraphLengthValueRange { get; protected set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public ValueRange ObstaclePercentValueRange { get; }

        public GraphCreatingModel(IMainModel model,
            IGraphFiller graphFactory)
        {
            this.model = model;
            this.graphFactory = graphFactory;
            ObstaclePercentValueRange = new ValueRange(99, 0);
        }

        public virtual void CreateGraph()
        {
            var graph = graphFactory.CreateGraph(ObstaclePercent, Width, Length);

            model.ConnectNewGraph(graph);
        }

        protected IMainModel model;
        protected IGraphFiller graphFactory;
    }
}
