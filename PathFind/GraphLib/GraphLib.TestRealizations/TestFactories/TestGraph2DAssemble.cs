using GraphLib.Interfaces;
using GraphLib.Realizations.Factories.CoordinateFactories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Factories.GraphFactories;
using GraphLib.Realizations.Factories.NeighboursCoordinatesFactories;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestFactories.Matrix;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraph2DAssemble : GraphAssemble
    {
        public TestGraph2DAssemble()
            : base(new TestVertexFactory(),
                  new Coordinate2DFactory(),
                  new Graph2DFactory(),
                  new TestCostFactory(),
                  new AroundNeighboursCoordinatesFactory())
        {

        }

        public override IGraph AssembleGraph(int obstaclePercent = 0, params int[] sizes)
        {
            var graph = (Graph2D)base.AssembleGraph(0, Constants.DimensionSizes2D);
            var matrices = new Matrices(new CostMatrix(graph), new ObstacleMatrix(graph));
            matrices.Overlay();
            return graph;
        }
    }
}
