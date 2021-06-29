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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="CantSerializeGraphException"></exception>
        public IGraph LoadGraph(Stream stream)
        {
            try
            {
                var verticesInfo = (GraphSerializationInfo)formatter.Deserialize(stream);
                var graph = graphFactory.CreateGraph(verticesInfo.DimensionsSizes);

                void CreateVertexFrom(VertexSerializationInfo info)
                {
                    graph[info.Position] = infoConverter.ConvertFrom(info);
                }

                verticesInfo.VerticesInfo.ForEach(CreateVertexFrom);

                graph.ConnectVertices();
                return graph;
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Saves graph in stream
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
        /// <exception cref="CantSerializeGraphException"></exception>
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
