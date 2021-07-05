using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace GraphLib.Serialization.Serializers
{
    public sealed class GraphSerializer : IGraphSerializer
    {
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
                var graphInfo = (GraphSerializationInfo)formatter.Deserialize(stream);
                var graph = graphFactory.CreateGraph(graphInfo.DimensionsSizes);
                void CreateVertexFrom(VertexSerializationInfo info)
                    => graph[info.Position] = infoConverter.ConvertFrom(info);
                graphInfo.VerticesInfo.ForEach(CreateVertexFrom);
                return graph.ConnectVertices();
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        public void SaveGraph(IGraph graph, Stream stream)
        {
            try
            {
                formatter.Serialize(stream, graph.GetGraphSerializationInfo());
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        private readonly IFormatter formatter;
        private readonly IVertexSerializationInfoConverter infoConverter;
        private readonly IGraphFactory graphFactory;
    }
}