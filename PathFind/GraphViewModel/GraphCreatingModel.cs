using GraphLib.Graphs.Factories.Interfaces;
using GraphViewModel.Interfaces;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public GraphCreatingModel(IMainModel model,
            IGraphFiller graphFactory)
        {
            this.model = model;
            this.graphFactory = graphFactory;
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
