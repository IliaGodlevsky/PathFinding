using GraphLib.Coordinates;
using GraphLib.GraphLib.Graphs.Serialization.Interface;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
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

            for (int i = 0; i < verticesDto.Width; i++)
            {
                for (int j = 0; j < verticesDto.Length; j++)
                {
                    for (int l = 0; l < verticesDto.Height; l++)
                    {
                        var indices = new Coordinate3D(i, j, l);
                        var index = Index.ToIndex(indices, verticesDto.Height, verticesDto.Length);
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
