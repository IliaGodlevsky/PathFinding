using System.IO;
using System.Runtime.Serialization;

namespace GraphLib.Serialization.Extensions
{
    internal static class FormatterExtensions
    {
        internal static GraphSerializationInfo DeserializeGraphInfo(this IFormatter self, Stream stream)
        {
            return (GraphSerializationInfo)self.Deserialize(stream);
        }
    }
}
