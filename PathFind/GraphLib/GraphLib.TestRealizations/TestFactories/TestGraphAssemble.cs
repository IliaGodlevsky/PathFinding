using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.TestRealizations.TestObjects;
using Random.Realizations.Generators;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraphAssemble : GraphAssemble<TestGraph, TestVertex>
    {
        public TestGraphAssemble()
            : base(new TestVertexFactory(),
                  new TestCoordinateFactory(),
                  new TestGraphFactory(),
                  new TestCostFactory(),
                  new VonNeumannNeighborhoodFactory(),
                  new PseudoRandom())
        {

        }
    }
}
