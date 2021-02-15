using Algorithm.Realizations.Tests.Objects;
using GraphLib.Interface;

namespace Algorithm.Realizations.Tests.Factories
{
    public class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
