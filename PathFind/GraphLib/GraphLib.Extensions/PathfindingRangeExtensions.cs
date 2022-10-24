using GraphLib.Interfaces;
using GraphLib.Proxy;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class PathfindingRangeExtensions
    {
        public static IEnumerable<IPathfindingRange> ToSubRanges(this IPathfindingRange self)
        {
            using (var iterator = self.GetEnumerator())
            {
                iterator.MoveNext();
                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    yield return new PathfindingRangeProxy(previous, current);
                    previous = iterator.Current;
                }
            }
        }

        public static bool IsInRange(this IPathfindingRange self, IVertex vertex)
        {
            return self.Contains(vertex);
        }

        public static bool IsIntermediate(this IPathfindingRange endPoints, IVertex vertex)
        {
            return endPoints.GetIntermediates().Any(v => v.IsEqual(vertex));
        }

        public static bool CanBeInPathfindingRange(this IPathfindingRange self, IVertex vertex)
        {
            return !vertex.IsIsolated() && !self.Contains(vertex);
        }

        public static bool HasSourceAndTargetSet(this IPathfindingRange self)
        {
            return !self.Source.IsIsolated() && !self.Target.IsIsolated();
        }

        public static bool HasIsolators(this IPathfindingRange self)
        {
            return self.Any(vertex => vertex.IsIsolated());
        }

        public static IEnumerable<IVertex> GetIntermediates(this IPathfindingRange self)
        {
            var vertices = new[] { self.Source, self.Target };
            return self.Where(item => !vertices.Contains(item));
        }
    }
}
