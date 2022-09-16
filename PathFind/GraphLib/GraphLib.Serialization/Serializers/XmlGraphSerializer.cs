using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace GraphLib.Serialization.Serializers
{
    public sealed class XmlGraphSerializer : GraphSerializer
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

        public XmlGraphSerializer(IVertexFromInfoFactory converter,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory)
            : base(converter, graphFactory, costFactory, coordinateFactory)
        {

        }

        protected override GraphSerializationInfo LoadGraphInternal(Stream stream,
            IVertexCostFactory costFactory, ICoordinateFactory coordinateFactory)
        {
            return XDocument.Load(stream).Root.GetGraphInfo(costFactory, coordinateFactory);
        }

        protected override void SaveGraphInternal(IGraph graph, Stream stream)
        {
            new XmlDocument().Append(new GraphSerializationInfo(graph)).Save(stream);
        }
    }
}
