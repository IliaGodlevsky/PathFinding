using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Serialization.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Serialization.Extensions
{
    internal static class VertexFromInfoFactoryExtensions
    {
        public static IReadOnlyCollection<IVertex> CreateManyFrom(this IVertexFromInfoFactory factory,
            IReadOnlyCollection<VertexSerializationInfo> info)
        {
            return info.Select(factory.CreateFrom).ToReadOnly();
        }
    }
}
