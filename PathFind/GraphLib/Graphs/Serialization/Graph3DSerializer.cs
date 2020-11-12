using GraphLib.Coordinates;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.IO;
using System.Linq;

namespace GraphLib.Graphs.Serialization
{
    public class Graph3DSerializer : BaseGraphSerializer
    {
        protected override IVertexInfoCollection Deserialize(Stream stream)
        {
            return (VertexInfoCollection3D)formatter.Deserialize(stream);
        }

        protected override IGraph GetGraphFromDto(IVertexInfoCollection verticesDto, 
            Func<VertexInfo, IVertex> dtoConverter)
        {
            var vertexInfoCollection = verticesDto as VertexInfoCollection3D;
            var width = vertexInfoCollection.Width;
            var length = vertexInfoCollection.Length;
            var height = vertexInfoCollection.Height;

            graph = new Graph3d(width, length, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    for (int z = 0; z < height; z++)
                    {
                        var indices = new Coordinate3D(x, y, z);
                        var index = Index.ToIndex(indices, length, height);
                        graph[indices] = dtoConverter(verticesDto.ElementAt(index));
                    }
                }
            }

            VertexConnector.ConnectVertices(graph);

            return graph;
        }
    }
}
