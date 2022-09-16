using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces.Factories;
using System;
using System.Linq;
using System.Xml.Linq;
using ValueRange;

using static GraphLib.Serialization.Serializers.XmlGraphSerializer;

namespace GraphLib.Serialization.Extensions
{
    internal static class XDocumentExtensions
    {
        public static GraphSerializationInfo GetGraphInfo(this XElement root,
            IVertexCostFactory costFactory, ICoordinateFactory coordinateFactory)
        {
            var dimensions = root.Element(Dimensions).Attributes<int>();

            var vertices = root.Element(Vertices)
                .Elements()
                .Select(element => element.GetVertex(costFactory, coordinateFactory))
                .ToReadOnly();

            var rangeValues = root.Element(Range).Attributes<int>();

            var range = new InclusiveValueRange<int>(rangeValues[0], rangeValues[1]);

            return new GraphSerializationInfo(dimensions, vertices, range);
        }

        private static VertexSerializationInfo GetVertex(this XElement element,
            IVertexCostFactory costFactory, ICoordinateFactory factory)
        {
            bool isObstacle = element.Element(Obstacle).Attribute<bool>(string.Format(Value, 0));

            int costValue = element.Element(Cost).Attribute<int>(string.Format(Value, 0));

            var cost = costFactory.CreateCost(costValue);

            var coordinateValues = element.Element(Coordinate).Attributes<int>();

            var coordinate = factory.CreateCoordinate(coordinateValues);

            var neighbours = element.Element(Neighbours)
                .Elements()
                .Select(Attributes<int>)
                .Select(factory.CreateCoordinate)
                .ToReadOnly();

            return new VertexSerializationInfo(isObstacle, cost, coordinate, neighbours);
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
