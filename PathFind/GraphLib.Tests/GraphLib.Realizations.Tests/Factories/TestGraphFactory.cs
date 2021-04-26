using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Tests.Objects;

namespace GraphLib.Realizations.Tests.Factories
{
    internal class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
