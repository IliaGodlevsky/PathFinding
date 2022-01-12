using GraphLib.Interfaces;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace GraphLib.Serialization.Extensions
{
    internal static class FormatterExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GraphSerializationInfo DeserializeGraph(this IFormatter self, Stream stream)
        {
            return (GraphSerializationInfo)self.Deserialize(stream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SerializeGraph(this IFormatter self, IGraph graph, Stream stream)
        {
            self.Serialize(stream, graph.ToGraphSerializationInfo());
        }
    }
}
