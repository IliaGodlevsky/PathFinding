using GraphLib.Realizations.CoordinateRadars;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestObjects;

namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal sealed class VertexMatrix : IMatrix
    {
        public VertexMatrix(Graph2D graph)
        {
            this.graph = graph;
        }

        public void Overlay()
        {
            for (int x = 0; x < graph.Width; x++)
            {
                for (int y = 0; y < graph.Length; y++)
                {
                    var coordinate = new Coordinate2D(x, y);
                    var coordinateRadar = new CoordinateAroundRadar(coordinate);
                    graph[coordinate] = new TestVertex(coordinateRadar, coordinate);
                }
            }
        }

        private readonly Graph2D graph;
    }
}
