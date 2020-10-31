using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using GraphLib.Vertex.Interface;
using GraphViewModel.Interfaces;
using System;

namespace GraphLib.ViewModel
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
            var graphfactory = new Graph2dFactory(new GraphParametres(Width, Height, ObstaclePercent));
            graph = graphfactory.CreateGraph(vertexFactory);
            model.ConnectNewGraph(graph);
        }

        protected IMainModel model;
        protected IGraph graph;
    }
}
