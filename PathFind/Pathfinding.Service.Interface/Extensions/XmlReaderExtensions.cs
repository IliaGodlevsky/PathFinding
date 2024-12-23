using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace Pathfinding.Service.Interface.Extensions
{
    public static class XmlReaderExtensions
    {
        public static T ReadElement<T>(this XmlReader reader, string elementName)
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == elementName)
            {
                var content = reader.ReadElementContentAsString();
                return (T)Convert.ChangeType(content, typeof(T));
            }

            if (reader.ReadToFollowing(elementName))
            {
                var content = reader.ReadElementContentAsString();
                return (T)Convert.ChangeType(content, typeof(T));
            }
            return default;
        }

        public static T ReadAttribute<T>(this XmlReader reader, string attributeName)
        {
            var value = reader.GetAttribute(attributeName);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static List<T> ReadCollection<T>(this XmlReader reader, string collectionName, string itemName) where T : IXmlSerializable, new()
        {
            var items = new List<T>();
            if (reader.NodeType == XmlNodeType.Element && reader.Name == collectionName)
            {
                if (reader.IsEmptyElement)
                {
                    reader.Read();
                    return items;
                }

                reader.ReadStartElement(collectionName);

                while (reader.NodeType != XmlNodeType.EndElement || reader.Name != collectionName)
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == itemName)
                    {
                        var item = new T();
                        item.ReadXml(reader);
                        items.Add(item);
                    }
                    else
                    {
                        reader.Read();
                    }
                }

                reader.ReadEndElement();
            }

            return items;
        }


        public static T? ReadNullableElement<T>(this XmlReader reader, string elementName) where T : struct
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == elementName)
            {
                var content = reader.ReadElementContentAsString();
                return !string.IsNullOrEmpty(content) ? (T)Convert.ChangeType(content, typeof(T)) : (T?)null;
            }

            if (reader.ReadToFollowing(elementName))
            {
                var content = reader.ReadElementContentAsString();
                return !string.IsNullOrEmpty(content) ? (T)Convert.ChangeType(content, typeof(T)) : (T?)null;
            }
            return null;
        }

        public static T? ReadNullableEnum<T>(this XmlReader reader, string elementName) where T : struct, Enum
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == elementName)
            {
                var content = reader.ReadElementContentAsString();
                return !string.IsNullOrEmpty(content) ? (T)Enum.Parse(typeof(T), content) : null;
            }

            if (reader.ReadToFollowing(elementName))
            {
                var content = reader.ReadElementContentAsString();
                return !string.IsNullOrEmpty(content) ? (T)Enum.Parse(typeof(T), content) : null;
            }

            return null;
        }
    }
}
