using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using GraphLib.Vertex.Interface;
using GraphViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public GraphCreatingModel(IMainModel model)
        {
            this.model = model;
        }

        public virtual void CreateGraph(Func<IVertex> vertexFactory)
        {
            var graphfactory = new GraphFactory<Graph2D>(ObstaclePercent, Width, Length);

            ICoordinate CoordinateFactory(IEnumerable<int> coordinates)
            {
                return new Coordinate2D(coordinates.ToArray());
            }

            var graph = graphfactory.CreateGraph(vertexFactory, CoordinateFactory);

            model.ConnectNewGraph(graph);
        }

        protected IMainModel model;
    }
}
