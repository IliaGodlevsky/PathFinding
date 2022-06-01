using System.Xml;

namespace Common.Extensions
{
    public static class XmlElementExtensions
    {
        public static void AppendAttributes(this XmlNode element, params XmlAttribute[] attriutes)
        {
            foreach (var attribute in attriutes)
            {
                element.Attributes.Append(attribute);
            }
        }

        public static void AppendChildren(this XmlNode element, params XmlNode[] nodes)
        {
            foreach (var node in nodes)
            {
                element.AppendChild(node);
            }
        }
    }
}
