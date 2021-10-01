using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestFactories.Matrix;

using static GraphLib.TestRealizations.Constants;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraph2DAssemble : IGraphAssemble
    {
        public IGraph AssembleGraph(int obstaclePercent = 0, params int[] sizes)
        {
            var graph = new Graph2D(DimensionSizes2D);
            var matrices = new Matrices(
                new VertexMatrix(graph),
                new CostMatrix(graph),
                new ObstacleMatrix(graph));
            matrices.Overlay();
            return graph.ConnectVertices();
        }
    }
}
