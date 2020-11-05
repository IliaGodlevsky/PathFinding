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

namespace GraphLib.GraphLib.Graphs.Serialization
{
    public class Graph2DSerializer : IGraphSerializer
    {
        public event Action<string> OnExceptionCaught;

        public Graph2DSerializer(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public Graph2DSerializer()
        {
            formatter = new BinaryFormatter();
        }

        public IGraph LoadGraph(string path, Func<VertexInfo, IVertex> converter)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var verticesDto = (VertexInfoCollection2D)formatter.Deserialize(stream);
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

        private IGraph GetGraphFromDto(VertexInfoCollection2D verticesDto,
            Func<VertexInfo, IVertex> dtoConverter)
        {
            graph = new Graph2d(verticesDto.Width, verticesDto.Length);

            for (int i = 0; i < verticesDto.Width; i++)
            {
                for (int j = 0; j < verticesDto.Length; j++)
                {
                    var indices = new Coordinate2D(i, j);
                    var index = Index.ToIndex(indices, verticesDto.Length);
                    graph[indices] = dtoConverter(verticesDto.ElementAt(index));
                }
            }

            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        private IGraph graph = new DefaultGraph();
        private readonly IFormatter formatter;
    }
}
