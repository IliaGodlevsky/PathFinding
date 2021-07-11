using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;

namespace GraphLib.TestRealizations.TestObjects
{
    public sealed class TestEndPoints : IEndPoints
    {
        public IVertex Target { get; }
        public IVertex Source { get; }

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
                   || vertex.IsEqual(Target)
                   || vertex.IsNull();
        }
    }
}
