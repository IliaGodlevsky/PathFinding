using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Proxy;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class EndPointsExtensions
    {
        public static IEnumerable<IEndPoints> ToSubEndPoints(this IEndPoints self)
        {
            using (var iterator = self.EndPoints.GetEnumerator())
            {
                iterator.MoveNext();
                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    yield return new EndPointsProxy(previous, current);
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
