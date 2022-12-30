using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using static Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.XmlAttributesNames;

namespace Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions
{
    internal static class XmlDocumentExtensions
    {
        public static XmlDocument Append(this XmlDocument document, GraphSerializationInfo info)
        {
            var child = document.CreateElement(Graph)
                .WithChildren(document.CreateNodes(info));
            document.AppendChild(child);
            return document;
        }

        private static IEnumerable<XmlNode> CreateNodes(this XmlDocument document, GraphSerializationInfo info)
        {
            yield return document.CreateElement(Dimensions)
                .WithAttributes(info.DimensionsSizes.AsEnumerable());
            yield return document.CreateNode(info.VerticesInfo);
        }

        private static XmlNode CreateNode(this XmlDocument document, IReadOnlyCollection<VertexSerializationInfo> infos)
        {
            return document.CreateElement(Vertices)
                .WithChildren(infos.Select(vertex => document.CreateElement(Vertex).WithChildren(document.CreateNodes(vertex))));
        }

        private static IEnumerable<XmlNode> CreateNodes(this XmlDocument document, VertexSerializationInfo vertex)
        {
            yield return document.CreateElement(Obstacle).WithAttributes(vertex.IsObstacle);
            yield return document.CreateElement(Neighbours).WithChildren(document.CreateNodes(vertex.Neighbourhood));
            yield return document.CreateElement(Coordinate).WithAttributes(vertex.Position.AsEnumerable());
            yield return document.CreateElement(Cost).WithAttributes(vertex.Cost.CurrentCost);
            yield return document.CreateElement(Range).WithAttributes(vertex.Cost.CostRange.LowerValueOfRange, vertex.Cost.CostRange.UpperValueOfRange);
        }

        private static IEnumerable<XmlNode> CreateNodes(this XmlDocument document, IEnumerable<ICoordinate> neighborhood)
        {
            return neighborhood.Select(neighbour => document.CreateElement(Coordinate).WithAttributes(neighbour.AsEnumerable()));
        }

        private static XmlNode WithAttributes<T>(this XmlNode node, params T[] array)
        {
            return node.WithAttributes(array.AsEnumerable());
        }

        private static XmlNode WithAttributes<T>(this XmlNode node, IEnumerable<T> array)
        {
            var root = node.OwnerDocument;
            int i = 0;
            foreach (var item in array)
            {
                string name = string.Format(Value, i);
                var attribute = root.CreateAttribute(name);
                var text = root.CreateTextNode(item.ToString());
                attribute.AppendChild(text);
                node.Attributes.Append(attribute);
                i++;
            }
            return node;
        }

        private static XmlNode WithChildren(this XmlNode node, IEnumerable<XmlNode> children)
        {
            children.ForEach(child => node.AppendChild(child));
            return node;
        }
    }
}