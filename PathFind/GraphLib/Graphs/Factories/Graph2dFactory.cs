using System;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using GraphLib.Coordinates;
using GraphLib.Graphs.Abstractions;
using GraphLib.Coordinates.Interface;
using System.Linq;
using GraphLib.Graphs.Factories.Abstractions;

namespace GraphLib.Graphs.Factories
{
    public class Graph2dFactory : BaseGraphFactory
    {
        public Graph2dFactory(GraphParametres parametres) : base(parametres.ObstaclePercent)
        {
            this.parametres = parametres;
        }

        public override IGraph CreateGraph(Func<IVertex> vertexFactory)
        {
            graph = new Graph2d(parametres.Width, parametres.Height);

            for (int i = 0; i < parametres.Width; i++)
            {
                for (int j = 0; j < parametres.Height; j++)
                {
                    CreateVertex(vertexFactory, i, j);
                }
            }

            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        protected override ICoordinate GetCoordinate(params int[] coordinates)
        {
            if (coordinates.Length != 2)
                throw new ArgumentException("Must be two coordinates");
            return new Coordinate2D(coordinates.First(), coordinates.Last());
        }


        private readonly GraphParametres parametres;
    }
}
