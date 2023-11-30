using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
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
        public static GraphSerializationInfo GetGraphInfo(this XElement root)
        {
            var dimensions = root.Element(Dimensions).Attributes<int>();
            var vertices = root.Element(Vertices)
                .Elements()
                .Select(element => element.GetVertex())
                .ToArray();
            return new(dimensions, vertices);
        }

        private static VertexSerializationInfo GetVertex(this XElement element)
        {
            bool isObstacle = element.Element(Obstacle).Attributes<bool>().Single();
            int costValue = element.Element(Cost).Attributes<int>().Single();
            var range = element.Element(Range).Attributes<int>().ToRange();
            var cost = new VertexCost(costValue, range);
            var coordinate = element.Element(Position).Attributes<int>().Create();
            var neighbours = element.Element(Neighbours)
                .Elements()
                .Select(Attributes<int>)
                .Select(ar=> new Coordinate(ar))
                .ToArray();

            return new(isObstacle, cost, coordinate, neighbours);
        }

        private static ICoordinate Create(this int[] values)
        {
            return new Coordinate(values);
        }

        private static InclusiveValueRange<int> ToRange(this int[] values)
        {
            return new(values.First(), values.Last());
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
