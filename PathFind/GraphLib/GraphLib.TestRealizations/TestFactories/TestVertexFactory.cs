using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestVertexFactory : IVertexFactory<TestVertex>
    {
        public TestVertex CreateVertex(ICoordinate coordinate)
        {
            return new TestVertex(coordinate);
        }
    }
}
