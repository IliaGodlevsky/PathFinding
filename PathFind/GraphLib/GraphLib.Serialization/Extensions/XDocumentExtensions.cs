using GraphLib.Interfaces.Factories;
using GraphLib.Proxy;
using System.Linq;
using System.Xml.Linq;
using ValueRange;

using static GraphLib.Serialization.Serializers.XmlGraphSerializer;

namespace GraphLib.Serialization.Extensions
{
    internal static class XDocumentExtensions
    {
        public static GraphSerializationInfo ToGraph(this XDocument document,
           IVertexCostFactory costFactory, ICoordinateFactory coordinateFactory)
        {
            return document.Root.GetGraphInfo(costFactory, coordinateFactory);
        }

        private static GraphSerializationInfo GetGraphInfo(this XElement root,
            IVertexCostFactory costFactory, ICoordinateFactory coordinateFactory)
        {
            var dimensions = root.Element(Dimensions).GetAttributes();

            var vertices = root.Element(Vertices)
                .Elements()
                .Select(element => element.GetVertex(costFactory, coordinateFactory))
                .ToArray();

            var rangeValues = root.Element(Range).GetAttributes();
            var range = new InclusiveValueRange<int>(rangeValues[0], rangeValues[1]);

            return new GraphSerializationInfo(dimensions, vertices, range);
        }

        private static VertexSerializationInfo GetVertex(this XElement element,
            IVertexCostFactory costFactory, ICoordinateFactory factory)
        {
            bool isObstacle = element.Element(Obstacle).Attribute(string.Format(Value, 0)).ParseBool();
            var cost = element.Element(Cost).Attribute(string.Format(Value, 0)).ParseInt();
            var coordinate = element.Element(Coordinate).GetAttributes();
            var neighbours = element.Element(Neighbours)
                .Elements()
                .Select(GetAttributes)
                .Select(factory.CreateCoordinate)
                .ToArray();

            return new VertexSerializationInfo(isObstacle, costFactory.CreateCost(cost),
                factory.CreateCoordinate(coordinate), new NeighbourhoodProxy(neighbours));
        }

        private static int[] GetAttributes(this XElement element)
        {
            return element.Attributes().Select(attr => attr.ParseInt()).ToArray();
        }

        private static bool ParseBool(this XAttribute attribute)
        {
            return bool.Parse(attribute.Value);
        }

        private static int ParseInt(this XAttribute attribute)
        {
            return int.Parse(attribute.Value);
        }
    }
}
