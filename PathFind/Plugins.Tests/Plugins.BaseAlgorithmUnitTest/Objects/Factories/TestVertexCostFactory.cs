using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories
{
    internal sealed class TestVertexCostFactory : IVertexCostFactory
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
