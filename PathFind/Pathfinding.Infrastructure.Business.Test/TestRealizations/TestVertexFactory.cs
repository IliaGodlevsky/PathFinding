using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal sealed class TestVertexFactory : IVertexFactory<TestVertex>
    {
        public TestVertex CreateVertex(Coordinate coordinate)
        {
            return new TestVertex(coordinate);
        }
    }
}
