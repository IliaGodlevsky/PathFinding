using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestEndPoints : IEndPoints
    {
        public IVertex Target { get; }

        public IVertex Source { get; }

        public IEnumerable<IVertex> EndPoints { get; }

        public TestEndPoints(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
            EndPoints = new[] { Source, Target };
        }

        public TestEndPoints(IGraph graph)
            : this(graph.Vertices.First(), graph.Vertices.Last())
        {

        }
    }
}
