using GraphLib.Coordinates;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Graphs.Serialization
{
    public class Graph3DSerializer : IGraphSerializer
    {
        public event Action<string> OnExceptionCaught;

        public Graph3DSerializer(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public Graph3DSerializer()
        {
            formatter = new BinaryFormatter();
        }

        public IGraph LoadGraph(string path, Func<VertexInfo, IVertex> converter)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var verticesDto = (VertexInfoCollection3D)formatter.Deserialize(stream);
                    graph = GetGraphFromDto(verticesDto, converter);
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }

            return graph;
        }

        public void SaveGraph(IGraph graph, string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(stream, graph.VertexInfoCollection);
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }
        }

        private IGraph GetGraphFromDto(VertexInfoCollection3D verticesDto,
            Func<VertexInfo, IVertex> dtoConverter)
        {
            graph = new Graph3d(verticesDto.Width, verticesDto.Length, verticesDto.Height);
            for (int x = 0; x < verticesDto.Width; x++)
            {
                for (int y = 0; y < verticesDto.Length; y++)
                {
                    for (int z = 0; z < verticesDto.Height; z++)
                    {
                        var indices = new Coordinate3D(x, y, z);
                        var index = Index.ToIndex(indices, verticesDto.Length, verticesDto.Height);
                        graph[indices] = dtoConverter(verticesDto.ElementAt(index));
                    }
                }
            }
            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        private IGraph graph = new DefaultGraph();
        private readonly IFormatter formatter;
    }
}
