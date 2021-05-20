using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrix;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories
{
    internal sealed class TestGraphAssemble : IGraphAssemble
    {
        public IGraph AssembleGraph(int obstaclePercent,
            params int[] sizes)
        {
            var graph = new TestGraph(sizes);
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
