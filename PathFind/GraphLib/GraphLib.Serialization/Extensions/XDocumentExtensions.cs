using GraphLib.Interfaces.Factories;
using GraphLib.Proxy;
using System;
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
            var dimensions = root.Element(Dimensions).Attributes<int>();

            var vertices = root.Element(Vertices)
                .Elements()
                .Select(element => element.GetVertex(costFactory, coordinateFactory))
                .ToArray();

            var rangeValues = root.Element(Range).Attributes<int>();
            var range = new InclusiveValueRange<int>(rangeValues[0], rangeValues[1]);

            return new GraphSerializationInfo(dimensions, vertices, range);
        }

        private static VertexSerializationInfo GetVertex(this XElement element,
            IVertexCostFactory costFactory, ICoordinateFactory factory)
        {
            bool isObstacle = element.Element(Obstacle).Attribute<bool>(string.Format(Value, 0));
            int cost = element.Element(Cost).Attribute<int>(string.Format(Value, 0));
            var coordinate = element.Element(Coordinate).Attributes<int>();
            var neighbours = element.Element(Neighbours).Elements()
                .Select(Attributes<int>).Select(factory.CreateCoordinate).ToArray();

            return new VertexSerializationInfo(isObstacle, costFactory.CreateCost(cost),
                factory.CreateCoordinate(coordinate), new NeighbourhoodProxy(neighbours));
        }

        private static T Attribute<T>(this XElement element, string name)
        {
            return element.Attribute(name).Parse<T>();
        }

        private static T[] Attributes<T>(this XElement element)
        {
            return element.Attributes().Select(Parse<T>).ToArray();
        }

        private static T Parse<T>(this XAttribute attribute)
        {
            return (T)Convert.ChangeType(attribute.Value, typeof(T));
        }
    }
}
