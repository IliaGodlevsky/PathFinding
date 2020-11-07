using GraphLib.Coordinates;
using GraphLib.Coordinates.Interface;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Abstractions;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.Linq;

namespace GraphLib.Graphs.Factories
{
    public class Graph3dFactory : BaseGraphFactory
    {
        public Graph3dFactory(int width, int length, 
            int height, int obstacleChance) : base(obstacleChance)
        {
            this.width = width;
            this.length = length;
            this.height = height;
        }

        public override IGraph CreateGraph(Func<IVertex> vertexFactory)
        {
            graph = new Graph3d(width, length, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int l = 0; l < height; l++)
                    {
                        CreateVertex(vertexFactory, i, j, l);
                    }
                }
            }

            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        protected override ICoordinate GetCoordinate(params int[] coordinates)
        {
            if (coordinates.Length != 3)
                throw new ArgumentException("Must be three coordinates");
            return new Coordinate3D(
                coordinates.ElementAt(0), 
                coordinates.ElementAt(1), 
                coordinates.ElementAt(2));
        }

        private readonly int width;
        private readonly int length;
        private readonly int height;
    }
}
