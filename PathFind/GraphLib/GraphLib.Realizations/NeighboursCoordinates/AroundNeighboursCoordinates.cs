using Common.Interface;
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
    public sealed class AroundNeighboursCoordinates : INeighboursCoordinates, ISerializable, ICloneable<INeighboursCoordinates>
    {
        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IReadOnlyCollection<ICoordinate> Coordinates => neighboursCoordinates.Value;

        public AroundNeighboursCoordinates(ICoordinate coordinate)
        {
            selfCoordinate = coordinate;
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            limitDepth = selfCoordinatesValues.Length;
            resultCoordinatesValues = new int[limitDepth];
            lateralOffsetMatrix = limitDepth == 0 ? EmptyOffsetMatrix : OffsetMatrix;
            neighboursCoordinates = new Lazy<IReadOnlyCollection<ICoordinate>>(GetNeighboursCoordinates);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(selfCoordinate), selfCoordinate, typeof(ICoordinate));
        }

        private AroundNeighboursCoordinates(SerializationInfo info, StreamingContext context)
            : this((ICoordinate)info.GetValue(nameof(selfCoordinate), typeof(ICoordinate)))
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
            var neighboursCoordinates = new List<ICoordinate>();
            foreach (int offset in lateralOffsetMatrix)
            {
                resultCoordinatesValues[depth] = selfCoordinatesValues[depth] + offset;
                if (depth < limitDepth - 1)
                {
                    neighboursCoordinates.AddRange(DetectNeighboursCoordinates(depth + 1));
                }
                else
                {
                    neighboursCoordinates.Add(resultCoordinatesValues.ToCoordinate());
                }
            }
            return neighboursCoordinates;
        }

        private List<ICoordinate> GetNeighboursCoordinates()
        {
            var coordinates = DetectNeighboursCoordinates();
            coordinates.Remove(selfCoordinate);
            return coordinates;
        }

        public INeighboursCoordinates Clone()
        {
            return new AroundNeighboursCoordinates(selfCoordinate.Clone());
        }

        private readonly ICoordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] selfCoordinatesValues;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] lateralOffsetMatrix;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighboursCoordinates;

        private static readonly int[] EmptyOffsetMatrix = Array.Empty<int>();
        private static readonly int[] OffsetMatrix = new int[] { -1, 0, 1 };
    }
}