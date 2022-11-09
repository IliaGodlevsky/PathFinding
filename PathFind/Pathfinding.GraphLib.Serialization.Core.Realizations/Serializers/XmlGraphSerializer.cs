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
        internal const string Value = "value-{0}";
        internal const string Obstacle = "obstacle";
        internal const string Range = "range";
        internal const string Neighbours = "neighbours";
        internal const string Coordinate = "coordinate";
        internal const string Graph = "graph";
        internal const string Cost = "cost";
    }

    public sealed class XmlGraphSerializer<TGraph, TVertex> : GraphSerializer<TGraph, TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        public XmlGraphSerializer(IVertexFromInfoFactory<TVertex> converter,
            IGraphFactory<TGraph, TVertex> graphFactory,
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

        protected override void SaveGraphInternal(IGraph<IVertex> graph, Stream stream)
        {
            new XmlDocument().Append(new GraphSerializationInfo(graph)).Save(stream);
        }
    }
}
