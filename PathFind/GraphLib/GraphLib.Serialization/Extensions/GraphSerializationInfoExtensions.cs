using System.Xml;

namespace GraphLib.Serialization.Extensions
{
    internal static class GraphSerializationInfoExtensions
    {
        public static XmlDocument ToXmlDocument(this GraphSerializationInfo info)
        {
            var document = new XmlDocument();
            document.Append(info);
            return document;
        }
    }
}
