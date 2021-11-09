using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraphAssemble : GraphAssemble
    {
        public TestGraphAssemble()
            : base(new TestVertexFactory(),
                  new TestCoordinateFactory(),
                  new TestGraphFactory(),
                  new TestCostFactory(),
                  new VonNeumannNeighborhoodFactory())
        {

        }
    }
}
