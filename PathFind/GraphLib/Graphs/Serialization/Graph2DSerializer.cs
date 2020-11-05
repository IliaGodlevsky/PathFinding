using GraphLib.Graphs;
using GraphLib.Coordinates;
using GraphLib.Info;
using GraphLib.Info.Containers;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Abstractions;
using GraphLib.Info.Interface;

namespace GraphLib.GraphLib.Graphs.Serialization
{
    public class Graph2DSerializer : BaseGraphSerializer
    {
        protected override IVertexInfoCollection Deserialize(Stream stream)
        {
            return (VertexInfoCollection2D)formatter.Deserialize(stream);
        }

        protected override IGraph GetGraphFromDto(IVertexInfoCollection verticesDto, Func<VertexInfo, IVertex> dtoConverter)
        {
            var width = (verticesDto as VertexInfoCollection2D).Width;
            var length = (verticesDto as VertexInfoCollection2D).Length;
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
