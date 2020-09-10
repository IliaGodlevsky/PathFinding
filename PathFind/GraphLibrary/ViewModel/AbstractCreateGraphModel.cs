using GraphLibrary.Constants;
using GraphLibrary.GraphCreate.GraphFactory.Interface;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System;

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

        public virtual void CreateGraph(Func<IVertex> generator)
        {
            IGraphFactory factory = new GraphFactory.GraphFactory(
                new GraphParametres(Width, Height, ObstaclePercent),
                VertexSize.SIZE_BETWEEN_VERTICES);
            graph = factory.GetGraph(generator);
            model.SetGraph(graph);
        }

        protected IMainModel model;
        protected Graph graph;
    }
}
