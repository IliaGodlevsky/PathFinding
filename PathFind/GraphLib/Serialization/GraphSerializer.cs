using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Serialization
{
    public class GraphSerializer : IGraphSerializer
    {
        public event Action<Exception> OnExceptionCaught;

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
        /// <exception cref="SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public IGraph LoadGraph(Stream stream)
        {
            try
            {
                var verticesInfo = (GraphSerializationInfo)formatter.Deserialize(stream);
                var dimensions = verticesInfo.DimensionsSizes.ToArray();
                var graph = graphFactory.CreateGraph(dimensions);
                verticesInfo.ForEach((info, i) => graph[i] = infoConverter.ConvertFrom(info));
                graph.ConnectVertices();
                return graph;
            }
            catch(Exception ex)
            {
                OnExceptionCaught?.Invoke(ex);
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public void SaveGraph(IGraph graph, Stream stream)
        {
            try
            {
                formatter.Serialize(stream, graph.GetGraphSerializationInfo());
            }
            catch(Exception ex)
            {
                OnExceptionCaught?.Invoke(ex);
                throw ex;
            }           
        }

        private readonly IFormatter formatter;
        private readonly IVertexSerializationInfoConverter infoConverter;
        private readonly IGraphFactory graphFactory;
    }
}
