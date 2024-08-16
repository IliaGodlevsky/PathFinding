using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Mock
{
    internal abstract class TestLayer<T> : ILayer
    {
        protected const int X = 30;
        protected const int Y = 35;

        private readonly Dictionary<Coordinate, T> matrix;

        protected abstract T[,] Matrix { get; }

        protected TestLayer()
        {
            matrix = GetDictionary();
        }

        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (var vertex in graph)
            {
                Assign(vertex, matrix[vertex.Position]);
            }
        }

        protected abstract void Assign(IVertex vertex, T value);

        private Dictionary<Coordinate, T> GetDictionary()
        {
            var dictionary = new Dictionary<Coordinate, T>();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    var coord = new Coordinate(i, j);
                    dictionary[coord] = Matrix[i, j];
                }
            }
            return dictionary;
        }
    }
}
