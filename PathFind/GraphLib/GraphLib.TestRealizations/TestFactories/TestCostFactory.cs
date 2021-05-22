using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestCostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost()
        {
            return new TestVertexCost();
        }

        public IVertexCost CreateCost(int cost)
        {
            return new TestVertexCost(cost);
        }
    }
}
