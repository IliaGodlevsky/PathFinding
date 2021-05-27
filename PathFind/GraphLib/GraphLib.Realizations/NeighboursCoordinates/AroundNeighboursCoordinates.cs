using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using static System.Linq.Enumerable;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    [Serializable]
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    public sealed class AroundNeighboursCoordinates : INeighboursCoordinates, ISerializable
    {
        public AroundNeighboursCoordinates(ICoordinate coordinate)
        {            
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            resultCoordinatesValues = new int[selfCoordinatesValues.Length];
            limitDepth = selfCoordinatesValues.Length;
            lateralNeighbourCoordinatesOffsets = new[] { -1, 0, 1 };
            neighboursCoordinates = new Lazy<IEnumerable<ICoordinate>>(GetNeighboursCoordinates);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(selfCoordinatesValues), selfCoordinatesValues, typeof(int[]));
        }

        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IEnumerable<ICoordinate> Coordinates => neighboursCoordinates.Value;

        private AroundNeighboursCoordinates(SerializationInfo info, StreamingContext context)
            : this(new Coordinate((int[])info.GetValue(nameof(selfCoordinatesValues), typeof(int[]))))
        {

        }

        // Recursive method
        private IEnumerable<ICoordinate> DetectNeighbourCoordinates(int depth = 0)
        {
            var neighbourCoordinates = new List<ICoordinate>();
            foreach (var offset in lateralNeighbourCoordinatesOffsets)
            {
                resultCoordinatesValues[depth] = selfCoordinatesValues[depth] + offset;
                if (depth < limitDepth - 1)
                {
                    neighbourCoordinates.AddRange(DetectNeighbourCoordinates(depth + 1));
                }
                else
                {
                    neighbourCoordinates.Add(new Coordinate(resultCoordinatesValues));
                }
            }
            return neighbourCoordinates;
        }

        private IEnumerable<ICoordinate> GetNeighboursCoordinates()
        {
            var selfCoordinate = new Coordinate(selfCoordinatesValues);
            return limitDepth == 0 ? Empty<ICoordinate>() : DetectNeighbourCoordinates().Except(selfCoordinate);
        }

        private readonly int[] selfCoordinatesValues;

        [NonSerialized]
        private readonly int limitDepth;
        [NonSerialized]
        private readonly int[] lateralNeighbourCoordinatesOffsets;
        [NonSerialized]
        private readonly int[] resultCoordinatesValues;
        [NonSerialized]
        private readonly Lazy<IEnumerable<ICoordinate>> neighboursCoordinates;
    }
}