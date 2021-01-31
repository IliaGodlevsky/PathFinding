using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Graphs.Serialization.Factories.Interfaces;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Graphs.Serialization.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Graphs.Serialization
{
    public class GraphSerializer : IGraphSerializer
    {
        public event Action<string> OnExceptionCaught;

        public GraphSerializer(IFormatter formatter,
            IVertexSerializationInfoConverter infoConverter,
            IGraphFactory graphFactory)
        {
            this.formatter = formatter;
            this.infoConverter = infoConverter;
            this.graphFactory = graphFactory;
        }

        public IGraph LoadGraph(Stream stream)
        {
            try
            {
                var verticesInfo = (GraphSerializationInfo)formatter.Deserialize(stream);
                var dimensions = verticesInfo.DimensionsSizes.ToArray();

                var graph = graphFactory.CreateGraph(dimensions);

                for (int i = 0; i < verticesInfo.Count(); i++)
                {
                    var vertexInfo = verticesInfo.ElementAt(i);
                    graph[i] = infoConverter.ConvertFrom(vertexInfo);
                }

                graph.ConnectVertices();

                return graph;

            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new NullGraph();
            }
        }

        public void SaveGraph(IGraph graph, Stream stream)
        {
            try
            {
                formatter.Serialize(stream, graph.SerializationInfo);
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }
        }

        private readonly IFormatter formatter;
        private readonly IVertexSerializationInfoConverter infoConverter;
        private readonly IGraphFactory graphFactory;
    }
}
