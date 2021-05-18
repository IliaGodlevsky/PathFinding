using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrix
{
    internal sealed class VertexMatrix : IMatrix
    {
        public VertexMatrix(TestGraph graph)
        {
            this.graph = graph;
        }

        public void Overlay()
        {
            for (int x = 0; x < graph.Width; x++)
            {
                for (int y = 0; y < graph.Length; y++)
                {
                    var coordinate = new TestCoordinate(x, y);
                    var coordinateRadar = new TestCoordinateAroundRadar(coordinate);
                    graph[coordinate] = new TestVertex(coordinateRadar, coordinate);
                }
            }
        }

        private readonly TestGraph graph;
    }
}
