using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories
{
    public class TestVertexFactory : IVertexFactory<TestVertex>
    {
        public TestVertex CreateVertex(ICoordinate coordinate)
        {
            return new TestVertex(coordinate);
        }
    }
}
