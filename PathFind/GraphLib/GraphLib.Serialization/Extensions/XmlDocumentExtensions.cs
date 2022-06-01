using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ValueRange.Extensions;

using static GraphLib.Serialization.Serializers.XmlGraphSerializer;

namespace GraphLib.Serialization.Extensions
{
    internal static class XmlDocumentExtensions
    {
        public static XmlDocument Append(this XmlDocument document, GraphSerializationInfo info)
        {
            return document.WithChild(() => document.CreateElement(Graph).WithChildren(() => document.CreateNodes(info)));
        }

        private static IEnumerable<XmlNode> CreateNodes(this XmlDocument document, GraphSerializationInfo info)
        {
            yield return document.CreateElement(Dimensions).WithAttributes(info.DimensionsSizes);
            yield return document.CreateNode(info.VerticesInfo);
            yield return document.CreateElement(Range).WithAttributes(info.CostRange.ToArray());
        }

        private static XmlNode CreateNode(this XmlDocument document, VertexSerializationInfo[] infos)
        {
            return document.CreateElement(Vertices).WithChildren(() =>
            {
                return infos.Select(vertex => document.CreateElement(Vertex).WithChildren(() => document.CreateNodes(vertex)));
            });
        }

        private static IEnumerable<XmlNode> CreateNodes(this XmlDocument document, VertexSerializationInfo vertex)
        {
            yield return document.CreateElement(Obstacle).WithAttributes(vertex.IsObstacle);
            yield return document.CreateElement(Neighbours).WithChildren(() => document.CreateNodes(vertex.Neighbourhood));
            yield return document.CreateElement(Coordinate).WithAttributes(vertex.Position.CoordinatesValues);
            yield return document.CreateElement(Cost).WithAttributes(vertex.Cost.CurrentCost);
        }

        private static IEnumerable<XmlNode> CreateNodes(this XmlDocument document, INeighborhood neighborhood)
        {
            return neighborhood.Neighbours
                .Select(neighbour => document.CreateElement(Coordinate)
                .WithAttributes(neighbour.CoordinatesValues));
        }

        private static XmlText CreateValue<T>(this XmlDocument document, T value)
        {
            return document.CreateTextNode(value.ToString());
        }

        private static XmlAttribute CreateAttributeWithValue<T>(this XmlDocument document, string name, T value)
        {
            return document.CreateAttribute(name).WithValue(() => document.CreateValue(value));
        }

        private static IEnumerable<XmlAttribute> GenerateAttributes<T>(this XmlDocument document, T[] array)
        {
            return array.Select((value, i) => document.CreateAttributeWithValue($"value-{i}", value));
        }

        private static Xml WithAttributes<Xml, T>(this Xml node, params T[] array)
            where Xml : XmlNode
        {
            node.AppendAttributes(node.OwnerDocument.GenerateAttributes(array).ToArray());
            return node;
        }

        private static Xml WithChild<Xml>(this Xml node, Func<XmlNode> child)
            where Xml : XmlNode
        {
            node.AppendChild(child.Invoke());
            return node;
        }

        private static Xml WithChildren<Xml>(this Xml node, Func<IEnumerable<XmlNode>> children)
            where Xml : XmlNode
        {
            node.AppendChildren(children.Invoke().ToArray());
            return node;
        }

        private static XmlAttribute WithValue(this XmlAttribute attribute, Func<XmlText> value)
        {
            var text = value.Invoke();
            attribute.AppendChild(text);
            return attribute;
        }
    }
}
