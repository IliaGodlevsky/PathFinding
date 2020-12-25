using Common.Extensions;
using GraphLib.Coordinates.Abstractions;
using System.Collections.Generic;
using System.Linq;
using static Common.ObjectActivator;

namespace GraphLib.Coordinates.Infrastructure
{
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    /// <typeparam name="TCoordinate"></typeparam>
    public sealed class CoordinateEnvironment<TCoordinate>
        where TCoordinate : class, ICoordinate
    {
        public CoordinateEnvironment(TCoordinate coordinate)
        {
            environment = new List<TCoordinate>();
            selfCoordinatesValue = coordinate.CoordinatesValues.ToArray();
            currentCoordinatesValues = new int[selfCoordinatesValue.Length];
            middleCoordinate = coordinate;
            limitDepth = selfCoordinatesValue.Length;
        }

        static CoordinateEnvironment()
        {
            var ctor = typeof(TCoordinate).GetConstructor(typeof(int[]));
            RegisterConstructor<TCoordinate>(ctor);
        }

        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IEnumerable<ICoordinate> GetEnvironment()
        {
            FormEnvironment();
            return environment;
        }

        // Recursive method
        private void FormEnvironment(int depth = 0)
        {
            var neighbours = GetNeighbourCoordinates(depth);
            foreach (var coordinate in neighbours)
            {
                currentCoordinatesValues[depth] = coordinate;
                if (CanMoveDeeper(depth))
                    FormEnvironment(depth + 1);
                else
                    AddNeighbourToEnvironment();
            }
        }

        private void AddNeighbourToEnvironment()
        {
            var activator = GetActivator<TCoordinate>();
            var coordinate = activator(currentCoordinatesValues);

            if (!middleCoordinate.Equals(coordinate))
            {
                environment.Add(coordinate);
            }
        }

        private IEnumerable<int> GetNeighbourCoordinates(int depth)
        {
            return new int[]
            {
                selfCoordinatesValue[depth] - 1,
                selfCoordinatesValue[depth],
                selfCoordinatesValue[depth] + 1
            };
        }

        private bool CanMoveDeeper(int depth)
        {
            return depth < limitDepth - 1;
        }

        private readonly TCoordinate middleCoordinate;

        private readonly int[] currentCoordinatesValues;
        private readonly int[] selfCoordinatesValue;

        private readonly List<TCoordinate> environment;
        private readonly int limitDepth;
    }
}
