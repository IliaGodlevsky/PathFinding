using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Tests.Objects;

namespace GraphLib.Serialization.Tests.Factories
{
    internal class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
