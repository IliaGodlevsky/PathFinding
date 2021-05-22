using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new TestGraph(dimensionSizes);
        }
    }
}
