using System;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexBinding;
using System.Drawing;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphCreate.GraphFactory.Interface;

namespace GraphLibrary.GraphFactory
{
    public class GraphFactory : IGraphFactory
    {
        public GraphFactory(GraphParametres parametres, int placeBetweenVertices)
        {
            this.parametres = parametres;
            this.placeBetweenVertices = placeBetweenVertices;
        }

        static GraphFactory()
        {
            rand = new Random();
        }


        public Graph GetGraph(Func<IVertex> generator)
        {
            graph = new Graph(parametres.Width, parametres.Height);

            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = graph.GetIndices(vertex);
                vertex = generator();
                vertex.Cost = rand.GetRandomValueCost();
                if (rand.IsObstacleChance(parametres.ObstaclePercent))
                    vertex.MarkAsObstacle();
                vertex.SetLocation(indices);
                return vertex;
            }

            graph.Array.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(graph);

            return graph;
        }

        private static readonly Random rand;

        private Graph graph;
        private readonly GraphParametres parametres;
        private readonly int placeBetweenVertices;

    }
}
