using GraphLib.Base;
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
            IVertexFromInfoFactory converter,
            IGraphFactory graphFactory)
        {
            this.formatter = formatter;
            this.vertexFactory = converter;
            this.graphFactory = graphFactory;
        }

        public IGraph LoadGraph(Stream stream)
        {
            try
            {
                var graphInfo = formatter.DeserializeGraph(stream);
                var vertices = graphInfo.CreateVertices(vertexFactory);
                BaseVertexCost.CostRange = graphInfo.CostRange;
                return graphFactory.CreateGraph(vertices, graphInfo.DimensionsSizes);
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
                formatter.SerializeGraph(graph, stream);
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }

        private readonly IFormatter formatter;
        private readonly IVertexFromInfoFactory vertexFactory;
        private readonly IGraphFactory graphFactory;
    }
}