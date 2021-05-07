using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects;

namespace Plugins.DijkstraAlgorithm.Tests.Objects.Factories
{
    internal sealed class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
