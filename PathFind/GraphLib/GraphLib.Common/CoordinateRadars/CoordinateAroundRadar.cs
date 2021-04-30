using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Common.CoordinateRadars
{
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    public sealed class CoordinateAroundRadar : ICoordinateRadar
    {
        public CoordinateAroundRadar(ICoordinate coordinate)
        {
            environment = new List<int[]>();
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            currentCoordinatesValues = new int[selfCoordinatesValues.Length];
            limitDepth = selfCoordinatesValues.Length;
            lateralNeighbourCoordinatesOffsets = new[] { -1, 0, 1 };
        }

        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IEnumerable<int[]> Environment
        {
            get
            {
                if (neighbours == null)
                {
                    FormEnvironment();
                    neighbours = environment;
                }
                return neighbours;
            }
        }

        // Recursive method
        private void FormEnvironment(int depth = 0)
        {
            lateralNeighbourCoordinatesOffsets
                .Select(i => selfCoordinatesValues[depth] + i)
                .ForEach(coordinate => TryMoveDeeper(depth, coordinate));
        }

        private void TryMoveDeeper(int depth, int coordinate)
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
        }

        private readonly int limitDepth;

        private readonly int[] lateralNeighbourCoordinatesOffsets;
        private readonly int[] currentCoordinatesValues;
        private readonly int[] selfCoordinatesValues;
        private readonly List<int[]> environment;

        private IEnumerable<int[]> neighbours;
    }
}