using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Primitives.ValueRange;
using System;
using System.Linq;
using System.Xml.Linq;

using static Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.XmlAttributesNames;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
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
                .ToArray();

            return new GraphSerializationInfo(dimensions, vertices);
        }

        private static VertexSerializationInfo GetVertex(this XElement element,
            IVertexCostFactory costFactory, ICoordinateFactory factory)
        {
            bool isObstacle = element.Element(Obstacle).Attribute<bool>(string.Format(Value, 0));

            int costValue = element.Element(Cost).Attribute<int>(string.Format(Value, 0));

            var rangeValues = element.Element(Range).Attributes<int>();

            var range = new InclusiveValueRange<int>(rangeValues[0], rangeValues[1]);

            var cost = costFactory.CreateCost(costValue, range);

            var coordinateValues = element.Element(Coordinate).Attributes<int>();

            var coordinate = factory.CreateCoordinate(coordinateValues);

            var neighbours = element.Element(Neighbours)
                .Elements()
                .Select(Attributes<int>)
                .Select(factory.CreateCoordinate)
                .ToArray();

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
