using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories
{
    public sealed class TestVertexFromInfoFactory : IVertexFromInfoFactory<TestVertex>
    {
        public TestVertex CreateFrom(VertexSerializationInfo info)
        {
            return new TestVertex(info.Position)
            {
                IsObstacle = info.IsObstacle,
                Cost = info.Cost
            };
        }
    }
}
