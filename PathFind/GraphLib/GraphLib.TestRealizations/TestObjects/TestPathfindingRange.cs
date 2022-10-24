using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestPathfindingRange : IPathfindingRange
    {
        public IVertex Target { get; }

        public IVertex Source { get; }

        public TestPathfindingRange(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public TestPathfindingRange(IGraph graph)
            : this(graph.First(), graph.Last())
        {

        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            yield return Source;
            yield return Target;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
