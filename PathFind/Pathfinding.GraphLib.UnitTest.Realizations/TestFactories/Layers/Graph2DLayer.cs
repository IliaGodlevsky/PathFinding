using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers
{
    public abstract class Graph2DLayer<T> : ILayer
    {
        private readonly T[,] matrix;

        protected Graph2DLayer()
        {
            matrix = CreateMatrix();
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            for (int x = 0; x < graph.GetWidth(); x++)
            {
                for (int y = 0; y < graph.GetLength(); y++)
                {
                    var coordinate = new Coordinate(x, y);
                    Assign(graph.Get(coordinate), matrix[x, y]);
                }
            }
        }

        protected abstract void Assign(IVertex vertex, T value);

        protected abstract T[,] CreateMatrix();
    }
}
