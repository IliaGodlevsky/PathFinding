using Pathfinding.GraphLib.Core.Interface;
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
            return new(dimensions, vertices);
        }

        private static VertexSerializationInfo GetVertex(this XElement element,
            IVertexCostFactory costFactory, ICoordinateFactory factory)
        {
            bool isObstacle = element.Element(Obstacle).Attributes<bool>().Single();
            int costValue = element.Element(Cost).Attributes<int>().Single();
            var range = element.Element(Range).Attributes<int>().ToRange();
            var cost = costFactory.CreateCost(costValue, range);
            var coordinate = element.Element(Coordinate).Attributes<int>().Create(factory);
            var neighbours = element.Element(Neighbours)
                .Elements()
                .Select(Attributes<int>)
                .Select(factory.CreateCoordinate)
                .ToArray();

            return new(isObstacle, cost, coordinate, neighbours);
        }

        private static ICoordinate Create(this int[] values, ICoordinateFactory factory)
        {
            return factory.CreateCoordinate(values);
        }

        private static InclusiveValueRange<int> ToRange(this int[] values)
        {
            return new (values.First(), values.Last());
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
