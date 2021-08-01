using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    [Serializable]
    public sealed class AroundNeighboursCoordinates : INeighboursCoordinates, ISerializable
    {
        public AroundNeighboursCoordinates(ICoordinate coordinate)
        {
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            limitDepth = selfCoordinatesValues.Length;
            resultCoordinatesValues = new int[limitDepth];
            lateralOffsetMatrix = limitDepth == 0 ? new int[] { } : new[] { -1, 0, 1 };
            neighboursCoordinates = new Lazy<ICoordinate[]>(GetNeighboursCoordinates);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(selfCoordinatesValues), selfCoordinatesValues, typeof(int[]));
        }

        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public ICoordinate[] Coordinates => neighboursCoordinates.Value;

        private AroundNeighboursCoordinates(SerializationInfo info, StreamingContext context)
            : this(((int[])info.GetValue(nameof(selfCoordinatesValues), typeof(int[]))).ToCoordinate())
        {

        }

        /// <summary>
        /// Detects neighbours coordinates around the central coordinate
        /// </summary>
        /// <param name="depth">The depth of the recursive dive</param>
        /// <remarks>Recursive method</remarks>
        /// <returns>An array of neighbours coordinates around the central one</returns>
        private List<ICoordinate> DetectNeighboursCoordinates(int depth = 0)
        {
            var neighbourCoordinates = new List<ICoordinate>();
            foreach (int offset in lateralOffsetMatrix)
            {
                resultCoordinatesValues[depth] = selfCoordinatesValues[depth] + offset;
                if (depth < limitDepth - 1)
                {
                    var coordinates = DetectNeighboursCoordinates(depth + 1);
                    neighbourCoordinates.AddRange(coordinates);
                }
                else
                {
                    var coordinate = resultCoordinatesValues.ToCoordinate();
                    neighbourCoordinates.Add(coordinate);
                }
            }
            return neighbourCoordinates;
        }

        private ICoordinate[] GetNeighboursCoordinates()
        {
            return DetectNeighboursCoordinates()
                .Except(selfCoordinatesValues.ToCoordinate())
                .ToArray();
        }

        private readonly int limitDepth;
        private readonly int[] selfCoordinatesValues;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] lateralOffsetMatrix;
        private readonly Lazy<ICoordinate[]> neighboursCoordinates;
    }
}