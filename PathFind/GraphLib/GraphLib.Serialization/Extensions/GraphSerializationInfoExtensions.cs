using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GraphLib.Serialization.Extensions
{
    internal static class GraphSerializationInfoExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IVertex> CreateVertices(this GraphSerializationInfo self, IVertexFromInfoFactory factory)
        {
            return self.VerticesInfo.Select(factory.CreateFrom);
        }
    }
}
