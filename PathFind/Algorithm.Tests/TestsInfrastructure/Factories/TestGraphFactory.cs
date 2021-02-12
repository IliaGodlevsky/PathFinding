using Algorithm.Tests.TestsInfrastructure.Objects;
using GraphLib.Interface;

namespace Algorithm.Tests.TestsInfrastructure.Factories
{
    public class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
