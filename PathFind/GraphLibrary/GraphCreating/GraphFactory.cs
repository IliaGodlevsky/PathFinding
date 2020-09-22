using System;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.GraphCreating.Interface;
using GraphLibrary.VertexConnecting;

namespace GraphLibrary.GraphCreating
{
    public class GraphFactory : IGraphFactory
    {
        public GraphFactory(GraphParametres parametres)
        {
            this.parametres = parametres;
        }

        static GraphFactory()
        {
            rand = new Random();
        }


        public IGraph CreateGraph(Func<IVertex> vertexFactory)
        {
            graph = new Graph(parametres.Width, parametres.Height);

            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = graph.GetIndices(vertex);
                vertex = vertexFactory();
                vertex.Cost = rand.GetRandomValueCost();
                if (rand.IsObstacleChance(parametres.ObstaclePercent))
                    vertex.MarkAsObstacle();
                vertex.Position = indices;
                return vertex;
            }

            graph.Array.Apply(InitializeVertex);
            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        private static readonly Random rand;

        private IGraph graph;
        private readonly GraphParametres parametres;
    }
}
