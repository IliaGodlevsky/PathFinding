using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Graphs.Serialization.Factories.Interfaces;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using System.Linq;

namespace GraphLib.Graphs.Serialization
{
    internal static class GraphAssembler
    {
        public static IGraph AssembleGraph(GraphSerializationInfo info,
            IGraphFactory graphFactory,
            IVertexSerializationInfoConverter infoConverter)
        {
            var dimensions = info.DimensionsSizes.ToArray();

            var graph = graphFactory.CreateGraph(dimensions);

            for (int i = 0; i < info.Count(); i++)
            {
                var vertexInfo = info.ElementAt(i);
                graph[i] = infoConverter.ConvertFrom(vertexInfo);
            }

            graph.ConnectVertices();

            return graph;
        }
    }
}
