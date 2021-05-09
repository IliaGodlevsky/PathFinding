using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrix;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories
{
    internal sealed class TestGraphAssemble : IGraphAssemble
    {
        public TestGraphAssemble(IGraphAssemble graphAssemble)
        {
            this.graphAssemble = graphAssemble;
        }

        public IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            var graph = graphAssemble.AssembleGraph(obstaclePercent, graphDimensionSizes);
            IMatrix matrices = new Matrices(new ObstacleMatrix(graph), new CostMatrix(graph));
            matrices.Overlay();
            return graph;
        }

        private readonly IGraphAssemble graphAssemble;
    }
}
