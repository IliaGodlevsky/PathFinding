using GraphLib.Coordinates.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates.Infrastructure
{
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    /// <typeparam name="TCoordinate"></typeparam>
    public sealed class CoordinateEnvironment
    {
        public CoordinateEnvironment(ICoordinate coordinate)
        {
            environment = new List<int[]>();
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            currentCoordinatesValues = new int[selfCoordinatesValues.Length];
            limitDepth = selfCoordinatesValues.Length;
        }

        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IEnumerable<int[]> GetEnvironment()
        {
            FormEnvironment();
            return environment;
        }

        // Recursive method
        private void FormEnvironment(int depth = 0)
        {
            foreach (var coordinate in GetNeighbourCoordinates(depth))
            {
                currentCoordinatesValues[depth] = coordinate;
                if (CanMoveDeeper(depth))
                {
                    FormEnvironment(depth + 1);
                }
                else if (!selfCoordinatesValues
                    .SequenceEqual(currentCoordinatesValues))
                {
                    environment.Add(currentCoordinatesValues.ToArray());
                }
            }
        }

        private IEnumerable<int> GetNeighbourCoordinates(int depth)
        {
            for (int i = -1; i <= 1; i++) 
                yield return selfCoordinatesValues[depth] + i;
        }

        private bool CanMoveDeeper(int depth)
        {
            return depth < limitDepth - 1;
        }

        private readonly int[] currentCoordinatesValues;
        private readonly int[] selfCoordinatesValues;

        private readonly List<int[]> environment;
        private readonly int limitDepth;
    }
}
