using Common.Extensions;
using GraphLib.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Common
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
            LateralNeighbourCoordinatesOffsets = new[] { -1, 0, 1 };
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
            LateralNeighbourCoordinatesOffsets
                .Select(i => selfCoordinatesValues[depth] + i)
                .ForEach(coordinate => TryMoveDeeper(depth, coordinate));
        }

        private bool TryMoveDeeper(int depth, int coordinate)
        {
            bool canMoveDeeper = depth < limitDepth - 1;
            currentCoordinatesValues[depth] = coordinate;

            if (canMoveDeeper)
            {
                FormEnvironment(depth + 1);
            }
            else if (!selfCoordinatesValues.SequenceEqual(currentCoordinatesValues))
            {
                environment.Add(currentCoordinatesValues.ToArray());
            }

            return canMoveDeeper;
        }

        private readonly int[] LateralNeighbourCoordinatesOffsets;

        private readonly int[] currentCoordinatesValues;
        private readonly int[] selfCoordinatesValues;

        private readonly List<int[]> environment;
        private readonly int limitDepth;
    }
}
