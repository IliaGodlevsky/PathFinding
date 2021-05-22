using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestFactories.Matrix;

namespace GraphLib.TestRealizations.TestFactories
{
    public sealed class TestGraph2DAssemble : IGraphAssemble
    {
        public IGraph AssembleGraph(int obstaclePercent,
            params int[] sizes)
        {
            var graph = new Graph2D(sizes);
            var matrices = new Matrices(
                new VertexMatrix(graph),
                new CostMatrix(graph),
                new ObstacleMatrix(graph));
            matrices.Overlay();
            graph.ConnectVertices();
            return graph;
        }
    }
}
