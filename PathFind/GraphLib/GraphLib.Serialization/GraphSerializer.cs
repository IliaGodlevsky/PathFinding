using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GraphLib.Serialization
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
        /// <exception cref="SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public IGraph LoadGraph(Stream stream)
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

        /// <summary>
        /// Saves graph in stream
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public void SaveGraph(IGraph graph, Stream stream)
        {
            formatter.Serialize(stream, graph.GetGraphSerializationInfo());
        }

        /// <summary>
        /// Saves graph in stream asynchronly
        /// </summary>
        /// <param name="graph"></param>
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
