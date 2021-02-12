using GraphLib.Interface;
using GraphLib.Tests.TestInfrastructure.Objects;

namespace GraphLib.Tests.TestInfrastructure.Factories
{
    internal class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
