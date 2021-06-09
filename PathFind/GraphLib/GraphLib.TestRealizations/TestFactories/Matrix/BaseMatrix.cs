using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;

namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal abstract class BaseMatrix<T> : IMatrix
    {
        protected BaseMatrix(Graph2D graph)
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
                    Assign(graph[coordinate], matrix[x, y]);
                }
            }
        }

        protected abstract void Assign(IVertex vertex, T value);

        protected T[,] matrix;
        protected readonly Graph2D graph;
    }
}
