using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestObjects;
using System;

namespace GraphLib.TestRealizations.TestFactories.Matrix
{
    internal abstract class BaseMatrix<T> : IMatrix
    {
        protected readonly Graph2D graph;
        protected readonly Lazy<T[,]> matrix;

        protected T[,] Matrix => matrix.Value;

        protected BaseMatrix(Graph2D graph)
        {
            this.graph = graph;
            matrix = new Lazy<T[,]>(CreateMatrix);
        }

        public void Overlay()
        {
            for (int x = 0; x < graph.Width; x++)
            {
                for (int y = 0; y < graph.Length; y++)
                {
                    var coordinate = new TestCoordinate(x, y);
                    Assign(graph.GetByCoordinate(coordinate), Matrix[x, y]);
                }
            }
        }

        protected abstract void Assign(IVertex vertex, T value);

        protected abstract T[,] CreateMatrix();
    }
}
