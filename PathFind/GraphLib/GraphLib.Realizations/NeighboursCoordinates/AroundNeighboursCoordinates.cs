using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using static System.Linq.Enumerable;

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
            lateralNeighbourCoordinatesOffsets = limitDepth == 0 ? Empty<int>() : new[] { -1, 0, 1 };
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
            : this(((int[])info.GetValue(nameof(selfCoordinatesValues), typeof(int[]))).ToCoordinate())
        {

        }

        /// <summary>
        /// Detects neighbours coordinates around the central coordinate
        /// </summary>
        /// <param name="depth">The depth of the recursive dive</param>
        /// <returns>An array of neighbours coordinates around the central one</returns>
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
                    neighbourCoordinates.Add(resultCoordinatesValues.ToCoordinate());
                }
            }
            return neighbourCoordinates;
        }

        private IEnumerable<ICoordinate> GetNeighboursCoordinates()
        {
            return DetectNeighbourCoordinates().Except(selfCoordinatesValues.ToCoordinate());
        }
       
        private readonly int limitDepth;
        private readonly int[] selfCoordinatesValues;
        private readonly int[] resultCoordinatesValues;
        private readonly IEnumerable<int> lateralNeighbourCoordinatesOffsets;
        private readonly Lazy<IEnumerable<ICoordinate>> neighboursCoordinates;
    }
}