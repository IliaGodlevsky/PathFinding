using GraphLibrary.DTO;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System;
using GraphLibrary.VertexBinding;
using GraphLibrary.GraphCreate.GraphFactory.Interface;
using GraphLibrary.Graphs.Interface;

namespace GraphLibrary.GraphFactory
{
    public class GraphInfoInitializer : IGraphInitializer
    {
        
        public GraphInfoInitializer(VertexDto[,] info)
        {
            vertexDtos = info;
            if (vertexDtos != null)
            {
                width = vertexDtos.Width();
                height = vertexDtos.Height();
            }
        }

        public IGraph GetGraph(Func<VertexDto, IVertex> generator)
        {
            graph = new Graph(width, height);

            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = graph.GetIndices(vertex);
                vertex = generator(vertexDtos[indices.X, indices.Y]);
                vertex.Position = indices;
                return vertex;
            }

            graph.Array.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(graph);
            return graph;
        }

        private IGraph graph;
        private readonly int width;
        private readonly int height;
        private readonly VertexDto[,] vertexDtos;
    }
}
