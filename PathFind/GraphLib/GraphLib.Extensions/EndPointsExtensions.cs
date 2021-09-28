using GraphLib.Extensions.Objects;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EndPointsExtensions
    {
        /// <summary>
        /// Forms end points from source, target and intermediate vertices
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEndPoints[] ToEndPoints(this IIntermediateEndPoints self)
        {
            var vertices = self.GetVertices().ToArray();
            IEndPoints EndPoint(int i) => new EndPoints(vertices[i], vertices[i + 1]);
            return Enumerable.Range(0, vertices.Length - 1).Select(EndPoint).ToArray();
        }

        public static bool CanBeEndPoint(this IEndPoints self, IVertex vertex)
        {
            return !self.IsEndPoint(vertex) && !vertex.IsIsolated();
        }

        public static bool HasSourceAndTargetSet(this IEndPoints self)
        {
            return !self.Source.IsIsolated() && !self.Target.IsIsolated();
        }

        /// <summary>
        /// Returns all vertices that are chosen as end points in an array
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<IVertex> GetVertices(this IIntermediateEndPoints self)
        {
            return self.IntermediateVertices.Prepend(self.Source).Append(self.Target);
        }

        /// <summary>
        /// Determins, whether any vertex that is chosen as end point is isolated
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasIsolators(this IIntermediateEndPoints self)
        {
            return self.GetVertices().Any(vertex => vertex.IsIsolated());
        }
    }
}
