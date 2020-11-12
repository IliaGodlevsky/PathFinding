using GraphLib.Graphs;
using GraphLib.Coordinates;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.IO;
using System.Linq;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;

namespace GraphLib.GraphLib.Graphs.Serialization
{
    public class Graph2DSerializer : BaseGraphSerializer
    {
        protected override IVertexInfoCollection Deserialize(Stream stream)
        {
            return (VertexInfoCollection2D)formatter.Deserialize(stream);
        }

        protected override IGraph GetGraphFromDto(IVertexInfoCollection verticesDto, 
            Func<VertexInfo, IVertex> dtoConverter)
        {
            var vertexInfoCollection = verticesDto as VertexInfoCollection2D;
            var width = vertexInfoCollection.Width;
            var length = vertexInfoCollection.Length;

            graph = new Graph2d(width, length);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    var indices = new Coordinate2D(i, j);
                    var index = Index.ToIndex(indices, length);
                    graph[indices] = dtoConverter(verticesDto.ElementAt(index));
                }
            }

            VertexConnector.ConnectVertices(graph);
            return graph;
        }
    }
}
