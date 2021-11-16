using Common.Extensions.EnumerableExtensions;
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
        public static IEnumerable<IEndPoints> ToLocalEndPoints(this IEndPoints self)
        {
            using (var iterator = self.EndPoints.GetEnumerator())
            {
                iterator.MoveNext();
                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    yield return new LocalEndPoints(previous, current);
                    previous = iterator.Current;
                }
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

        /// <summary>
        /// Determins, whether any vertex that is chosen as end point is isolated
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HasIsolators(this IEndPoints self)
        {
            return self.EndPoints.Any(vertex => vertex.IsIsolated());
        }

        public static IEnumerable<IVertex> GetIntermediates(this IEndPoints self)
        {
            return self.EndPoints.Without(self.Source, self.Target);
        }
    }
}
