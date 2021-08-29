using GraphLib.Extensions.Objects;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EndPointsExtensions
    {
        public static IEnumerable<IEndPoints> ToEndPoints(this IIntermediateEndPoints self)
        {
            var vertices = self.GetVertices().ToArray();

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                yield return new EndPoints(vertices[i], vertices[i + 1]);
            }
        }

        public static bool CanBeEndPoint(this IEndPoints self, IVertex vertex)
        {
            return !self.IsEndPoint(vertex) && !vertex.IsIsolated();
        }

        public static bool HasSourceAndTargetSet(this IEndPoints self)
        {
            return !self.Source.IsIsolated() && !self.Target.IsIsolated();
        }

        public static IEnumerable<IVertex> GetVertices(this IIntermediateEndPoints self)
        {
            return self.IntermediateVertices.Prepend(self.Source).Append(self.Target);
        }

        public static bool HasIsolators(this IIntermediateEndPoints self)
        {
            return self.GetVertices().Any(vertex => vertex.IsIsolated());
        }
    }
}
