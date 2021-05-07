using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrices;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories
{
    internal sealed class TestGraphAssemble
    {
        public TestGraphAssemble(IGraphAssemble graphAssemble)
        {
            this.graphAssemble = graphAssemble;
        }

        public IGraph AssembleGraph()
        {
            var graph = graphAssemble.AssembleGraph(0, Constants.Width, Constants.Length);
            costMatrix = new CostMatrix(graph);
            obstacleMatrix = new ObstacleMatrix(graph);
            costMatrix.Overlay();
            obstacleMatrix.Overlay();
            return graph;
        }

        private readonly IGraphAssemble graphAssemble;
        private CostMatrix costMatrix;
        private ObstacleMatrix obstacleMatrix;
    }
}
