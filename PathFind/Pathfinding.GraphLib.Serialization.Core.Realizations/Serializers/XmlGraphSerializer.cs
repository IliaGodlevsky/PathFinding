using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers
{
    internal static class XmlAttributesNames
    {
        internal const string Dimensions = "dimensions";
        internal const string Vertices = "vertices";
        internal const string Vertex = "vertex";
        internal const string Range = "range";
        internal const string Value = "value-{0}";
        internal const string Obstacle = "obstacle";
        internal const string Neighbours = "neighbours";
        internal const string Coordinate = "coordinate";
        internal const string Graph = "graph";
        internal const string Cost = "cost";
    }

    public sealed class XmlGraphSerializer<TVertex> : GraphSerializer<TVertex>
        where TVertex : IVertex
    {
        public XmlGraphSerializer(IVertexFromInfoFactory<TVertex> converter,
            IGraphFactory<TVertex> graphFactory,
            IVertexCostFactory costFactory,
            ICoordinateFactory coordinateFactory)
            : base(converter, graphFactory, costFactory, coordinateFactory)
        {

        }

        protected override GraphSerializationInfo DeserializeInternal(Stream stream)
        {
            return XDocument.Load(stream).Root.GetGraphInfo(costFactory, coordinateFactory);
        }

        protected override void SerializeInternal(GraphSerializationInfo info, Stream stream)
        {
            new XmlDocument().Append(info).Save(stream);
        }
    }
}
