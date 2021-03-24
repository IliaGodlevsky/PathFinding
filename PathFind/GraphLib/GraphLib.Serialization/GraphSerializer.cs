using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GraphLib.Serialization
{
    public sealed class GraphSerializer : IGraphSerializer
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
                void CreateVertexFrom(VertexSerializationInfo info, int vertexIndexInGraph)
                {
                    graph[vertexIndexInGraph] = infoConverter.ConvertFrom(info);
                }
                verticesInfo.ForEach(CreateVertexFrom);
                graph.ConnectVerticesParallel();
                return graph;
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex);
                throw ex;
            }

        }

        /// <summary>
        /// Saves graph in stream
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
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Saves graph in stream asynchronly
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public async Task SaveGraphAsync(IGraph graph, Stream stream)
        {
            await Task.Run(() => SaveGraph(graph, stream));
        }

        private readonly IFormatter formatter;
        private readonly IVertexSerializationInfoConverter infoConverter;
        private readonly IGraphFactory graphFactory;
    }
}
