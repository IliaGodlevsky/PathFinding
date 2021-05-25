using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    [Serializable]
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    public sealed class AroundNeighboursCoordinates : INeighboursCoordinates
    {
        public AroundNeighboursCoordinates(ICoordinate coordinate)
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
        public IEnumerable<ICoordinate> Coordinates
        {
            get
            {
                if (neighboursCoordinates == null)
                {
                    neighboursCoordinates = new List<ICoordinate>();
                    if (limitDepth <= 0)
                    {
                        return neighboursCoordinates;
                    }
                    FormNeighboursCoordinates();
                }
                return neighboursCoordinates;
            }
        }

        // Recursive method
        private void FormNeighboursCoordinates(int depth = 0)
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
                FormNeighboursCoordinates(depth + 1);
            }
            else
            {
                var seldCoordinates = new Coordinate(selfCoordinatesValues);
                var resultCoordinates = new Coordinate(resultCoordinatesValues);
                if (!seldCoordinates.Equals(resultCoordinates))
                {
                    neighboursCoordinates.Add(resultCoordinates);
                }
            }


        }

        private readonly int limitDepth;

        private readonly int[] lateralNeighbourCoordinatesOffsets;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] selfCoordinatesValues;

        [NonSerialized]
        private List<ICoordinate> neighboursCoordinates;
    }
}