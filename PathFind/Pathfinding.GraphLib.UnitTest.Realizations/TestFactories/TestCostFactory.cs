using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories
{
    public class TestCostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost(int cost, InclusiveValueRange<int> range)
        {
            return new TestVertexCost(cost);
        }
    }
}
