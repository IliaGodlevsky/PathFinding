using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Exceptions;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace GraphLib.Serialization.Serializers
{
    public sealed class XmlGraphSerializer : IGraphSerializer
    {
        internal const string Dimensions = "dimensions";
        internal const string Vertices = "vertices";
        internal const string Vertex = "vertex";
        internal const string Value = "value-{0}";
        internal const string Obstacle = "obstacle";
        internal const string Range = "range";
        internal const string Neighbours = "neighbours";
        internal const string Coordinate = "coordinate";
        internal const string Graph = "graph";
        internal const string Cost = "cost";

        private readonly IVertexFromInfoFactory vertexFactory;
        private readonly IGraphFactory graphFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly ICoordinateFactory coordinateFactory;

        public XmlGraphSerializer(IVertexFromInfoFactory converter,
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
                var graphInfo = XDocument.Load(stream).ToGraph(costFactory, coordinateFactory);
                var vertices = graphInfo.CreateVertices(vertexFactory).ToArray();
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
                new XmlDocument().ToXml(graph.ToGraphSerializationInfo()).Save(stream);
            }
            catch (Exception ex)
            {
                throw new CantSerializeGraphException(ex.Message, ex);
            }
        }
    }
}
