using GraphLibrary.GraphCreating;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System;

namespace GraphLibrary.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ObstaclePercent { get; set; }

        public GraphCreatingModel(IMainModel model)
        {
            this.model = model;
        }

        public virtual void CreateGraph(Func<IVertex> vertexFactory)
        {
            var graphfactory = new GraphFactory(new GraphParametres(Width, Height, ObstaclePercent));
            graph = graphfactory.CreateGraph(vertexFactory);
            model.ConnectNewGraph(graph);
        }

        protected IMainModel model;
        protected IGraph graph;
    }
}
