using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers
{
    internal abstract class Graph2DLayer<T> : ILayer<Graph2D<TestVertex>, TestVertex>
    {
        private readonly T[,] matrix;

        protected Graph2DLayer()
        {
            matrix = CreateMatrix();
        }

        public void Overlay(Graph2D<TestVertex> graph)
        {
            for (int x = 0; x < graph.Width; x++)
            {
                for (int y = 0; y < graph.Length; y++)
                {
                    var coordinate = new TestCoordinate(x, y);
                    Assign(graph.Get(coordinate), matrix[x, y]);
                }
            }
        }

        protected abstract void Assign(IVertex vertex, T value);

        protected abstract T[,] CreateMatrix();
    }
}
