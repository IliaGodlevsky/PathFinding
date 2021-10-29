using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization.Extensions
{
    internal static class GraphSerializationInfoExtensions
    {
        public static IEnumerable<IVertex> CreateVertices(this GraphSerializationInfo self, IVertexFromInfoFactory factory)
        {
            return self.VerticesInfo.Select(factory.CreateFrom);
        }
    }
}
