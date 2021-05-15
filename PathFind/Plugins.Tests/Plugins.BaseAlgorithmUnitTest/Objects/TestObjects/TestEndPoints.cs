using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestEndPoints : IEndPoints
    {
        public TestEndPoints(IVertex start, IVertex end)
        {
            Target = end;
            Source = start;
        }

        public TestEndPoints()
        {
            Source = new NullVertex();
            Target = new NullVertex();
        }

        public IVertex Target { get; }

        public IVertex Source { get; }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(Target) || vertex.IsEqual(Source);
        }
    }
}
