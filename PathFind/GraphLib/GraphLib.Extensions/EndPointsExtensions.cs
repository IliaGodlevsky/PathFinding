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
            using (var iterator = self.GetEnumerator())
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

        public static bool IsEndPoint(this IEndPoints self, IVertex vertex)
        {
            return self.Contains(vertex);
        }

        public static bool IsIntermediate(this IEndPoints endPoints, IVertex vertex)
        {
            return endPoints.GetIntermediates().Any(v => v.IsEqual(vertex));
        }

        public static bool CanBeEndPoint(this IEndPoints self, IVertex vertex)
        {
            return !vertex.IsIsolated() && !self.IsEndPoint(vertex);
        }

        public static bool HasSourceAndTargetSet(this IEndPoints self)
        {
            return !self.Source.IsIsolated() && !self.Target.IsIsolated();
        }

        public static bool HasIsolators(this IEndPoints self)
        {
            return self.Any(vertex => vertex.IsIsolated());
        }

        public static IEnumerable<IVertex> GetIntermediates(this IEndPoints self)
        {
            var vertices = new[] { self.Source, self.Target };
            return self.Where(item => !vertices.Contains(item));
        }
    }
}
