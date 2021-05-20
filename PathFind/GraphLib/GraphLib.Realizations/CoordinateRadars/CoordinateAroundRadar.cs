using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.CoordinateRadars
{
    [Serializable]
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    public sealed class CoordinateAroundRadar : ICoordinateRadar
    {
        public CoordinateAroundRadar(ICoordinate coordinate)
        {
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            resultCoordinatesValues = new int[selfCoordinatesValues.Length];
            limitDepth = selfCoordinatesValues.Length;
            lateralNeighbourCoordinatesOffsets = new[] { -1, 0, 1 };
        }

        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IEnumerable<ICoordinate> Environment
        {
            get
            {
                if (environment == null)
                {
                    environment = new List<ICoordinate>();
                    FormEnvironment();
                }
                return environment;
            }
        }

        // Recursive method
        private void FormEnvironment(int depth = 0)
        {
            foreach (var offset in lateralNeighbourCoordinatesOffsets)
            {
                TryMoveDeeper(depth, selfCoordinatesValues[depth] + offset);
            }
        }

        private void TryMoveDeeper(int depth, int coordinate)
        {
            bool canMoveDeeper = depth < limitDepth - 1;
            resultCoordinatesValues[depth] = coordinate;

            if (canMoveDeeper)
            {
                FormEnvironment(depth + 1);
            }
            else if (!selfCoordinatesValues.SequenceEqual(resultCoordinatesValues))
            {
                environment.Add(new Coordinate(resultCoordinatesValues));
            }
        }

        private readonly int limitDepth;

        private readonly int[] lateralNeighbourCoordinatesOffsets;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] selfCoordinatesValues;

        private List<ICoordinate> environment;
    }
}