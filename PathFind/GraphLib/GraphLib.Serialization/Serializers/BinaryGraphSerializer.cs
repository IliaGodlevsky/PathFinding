using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphLib.Serialization.Serializers
{
    public sealed class BinaryGraphSerializer : IGraphSerializer
    {
        private readonly IVertexFromInfoFactory vertexFactory;
        private readonly IGraphFactory graphFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly ICoordinateFactory coordinateFactory;

        public BinaryGraphSerializer(IVertexFromInfoFactory converter,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory)
        {
            this.vertexFactory = converter;
            this.graphFactory = graphFactory;
            this.costFactory = costFactory;
            this.coordinateFactory = coordinateFactory;
        }

        public IGraph LoadGraph(Stream stream)
        {
            try
            {
                using (var reader = new BinaryReader(stream, Encoding.Default, leaveOpen: true))
                {
                    var graphInfo = reader.ReadGraph(costFactory, coordinateFactory);
                    var vertices = graphInfo.CreateVertices(vertexFactory).ToArray();
                    BaseVertexCost.CostRange = graphInfo.CostRange;
                    return graphFactory.CreateGraph(vertices, graphInfo.DimensionsSizes);
                }
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
                using (var writer = new BinaryWriter(stream, Encoding.Default, leaveOpen: true))
                {
                    writer.WriteGraph(graph);
                }
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }
    }
}
