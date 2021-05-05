using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Exceptions;
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
        public event Action<Exception, string> OnExceptionCaught;

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
                var dimensions = verticesInfo.DimensionsSizes.ToArray();
                var graph = graphFactory.CreateGraph(dimensions);

                void CreateVertexFrom(VertexSerializationInfo info)
                {
                    graph[info.Position] = infoConverter.ConvertFrom(info);
                }

                verticesInfo.VerticesInfo.ForEach(CreateVertexFrom);
                graph.ConnectVertices();
                return graph;
            }
            catch (SystemException ex)
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
            catch (SystemException ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Saves graph in stream async
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
        public async Task SaveGraphAsync(IGraph graph, Stream stream)
        {
            await Task.Run(() =>
            {
                try
                {
                    SaveGraph(graph, stream);
                }
                catch (CantSerializeGraphException ex)
                {
                    OnExceptionCaught?.Invoke(ex, string.Empty);
                }
            });
        }

        private readonly IFormatter formatter;
        private readonly IVertexSerializationInfoConverter infoConverter;
        private readonly IGraphFactory graphFactory;
    }
}
