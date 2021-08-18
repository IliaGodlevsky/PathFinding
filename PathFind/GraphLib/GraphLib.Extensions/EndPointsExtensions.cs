using GraphLib.Extensions.Objects;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EndPointsExtensions
    {
        public static IEnumerable<IEndPoints> ToIntermediateEndPoints(this IIntermediateEndPoints self)
        {
            var vertices = self.IntermediateVertices
                .Prepend(self.Source)
                .Append(self.Target)
                .ToArray();

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                yield return new EndPoints(vertices[i], vertices[i + 1]);
            }
        }
    }
}
