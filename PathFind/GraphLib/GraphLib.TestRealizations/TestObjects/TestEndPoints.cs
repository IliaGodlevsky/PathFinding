using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestEndPoints : IIntermediateEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }

        public IReadOnlyCollection<IVertex> IntermediateVertices => new IVertex[] { };

        public TestEndPoints(IVertex source, IVertex target)
        {
            Source = source;
            Target = target;
        }

        public TestEndPoints()
        {
            Source = new NullVertex();
            Target = new NullVertex();
        }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Source)
                   || vertex.IsEqual(Target);
        }
    }
}
