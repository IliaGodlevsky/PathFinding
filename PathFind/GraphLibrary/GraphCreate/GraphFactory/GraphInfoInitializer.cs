using GraphLibrary.DTO;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using System.Drawing;
using System;
using GraphLibrary.VertexBinding;
using GraphLibrary.GraphCreate.GraphFactory.Interface;

namespace GraphLibrary.GraphFactory
{
    public class GraphInfoInitializer : IGraphInitializer
    {
        
        public GraphInfoInitializer(VertexDto[,] info, 
            int placeBetweenVertices)
        {
            this.placeBetweenVertices = placeBetweenVertices;
            vertexDtos = info;
            if (vertexDtos != null)
            {
                width = vertexDtos.Width();
                height = vertexDtos.Height();
            }
        }

        public Graph GetGraph(Func<VertexDto, IVertex> generator)
        {
            graph = new Graph(width, height);

            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = graph.GetIndices(vertex);
                vertex = generator(vertexDtos[indices.X, indices.Y]);
                vertex.SetLocation(indices);
                return vertex;
            }

            graph.Array.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(graph);
            return graph;
        }

        private Graph graph;
        private readonly int placeBetweenVertices;
        private readonly int width;
        private readonly int height;
        private readonly VertexDto[,] vertexDtos;
    }
}
