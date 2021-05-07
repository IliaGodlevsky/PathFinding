using GraphLib.Interfaces;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;

namespace Plugins.BaseAlgorithmUnitTest.Objects.Factories.Matrices
{
    internal abstract class BaseMatrix<T>
    {
        protected BaseMatrix(IGraph graph)
        {
            this.graph = graph;
        }

        protected abstract void Assign(IVertex vertex, T value);

        public void Overlay()
        {
            for (int i = 0; i < Constants.Width; i++)
            {
                for (int j = 0; j < Constants.Length; j++)
                {
                    var coordinate = new TestCoordinate(i, j);
                    Assign(graph[coordinate], matrix[i, j]);
                }
            }
        }

        protected T[,] matrix;
        protected readonly IGraph graph;
    }
}
