using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestEndPoints : IEndPoints
    {
        public TestEndPoints(IVertex start, IVertex end)
        {
            End = end;
            Start = start;
        }

        public TestEndPoints()
        {
            Start = new NullVertex();
            End = new NullVertex();
        }

        public IVertex End { get; }

        public IVertex Start { get; }

        public bool IsEndPoint(IVertex vertex)
        {
            return vertex.IsEqual(End) || vertex.IsEqual(Start);
        }
    }
}
