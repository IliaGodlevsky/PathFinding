using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Graphs.Serialization.Abstractions
{
    public abstract class BaseGraphSerializer : IGraphSerializer
    {
        public event Action<string> OnExceptionCaught;

        public BaseGraphSerializer(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public BaseGraphSerializer()
        {
            graph = new DefaultGraph();
            formatter = new BinaryFormatter();
        }

        public IGraph LoadGraph(string path, Func<VertexInfo, IVertex> vertexFactory)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var verticesDto = Deserialize(stream);
                    graph = GetGraphFromDto(verticesDto, vertexFactory);
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }

            return graph;
        }

        protected abstract IVertexInfoCollection Deserialize(Stream stream);

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

        protected abstract IGraph GetGraphFromDto(IVertexInfoCollection verticesDto,
                       Func<VertexInfo, IVertex> dtoConverter);

        protected IGraph graph;
        protected IFormatter formatter;
    }
}
