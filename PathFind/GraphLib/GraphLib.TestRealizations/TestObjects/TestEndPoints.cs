using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestObjects
{
    public readonly struct TestEndPoints : IIntermediateEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }

        public IReadOnlyCollection<IVertex> IntermediateVertices => new IVertex[] { };

        public TestEndPoints(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Source)
                   || vertex.IsEqual(Target);
        }
    }
}
