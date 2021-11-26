using Common.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.Neighbourhoods
{
    /// <summary>
    /// A class that finds the neighbors of the specified coordinate
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Count = {Neighbours.Length}")]
    public sealed class MooreNeighborhood : INeighborhood, ISerializable, ICloneable<INeighborhood>
    {
        /// <summary>
        /// Returns an array of the coordinate neighbours
        /// </summary>
        /// <returns>An array of the coordinate neighbours</returns>
        public IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public MooreNeighborhood(ICoordinate coordinate)
        {
            selfCoordinate = coordinate;
            selfCoordinatesValues = coordinate.CoordinatesValues.ToArray();
            limitDepth = selfCoordinatesValues.Length;
            resultCoordinatesValues = new int[limitDepth];
            lateralOffsetMatrix = limitDepth == 0 ? EmptyOffsetMatrix : OffsetMatrix;
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(GetNeighborhood);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(selfCoordinate), selfCoordinate, typeof(ICoordinate));
        }

        private MooreNeighborhood(SerializationInfo info, StreamingContext context)
            : this((ICoordinate)info.GetValue(nameof(selfCoordinate), typeof(ICoordinate)))
        {

        }

        /// <summary>
        /// Detects neighbours coordinates around the central coordinate
        /// </summary>
        /// <param name="depth">The depth of the recursive dive</param>
        /// <remarks>Recursive method</remarks>
        /// <returns>An array of neighbours coordinates around the central one</returns>
        private List<ICoordinate> DetectNeighborhood(int depth = 0)
        {
            var neighborhood = new List<ICoordinate>();
            foreach (int offset in lateralOffsetMatrix)
            {
                resultCoordinatesValues[depth] = selfCoordinatesValues[depth] + offset;
                if (depth < limitDepth - 1)
                {
                    neighborhood.AddRange(DetectNeighborhood(depth + 1));
                }
                else
                {
                    neighborhood.Add(resultCoordinatesValues.ToCoordinate());
                }
            }
            return neighborhood;
        }

        private List<ICoordinate> GetNeighborhood()
        {
            var coordinates = DetectNeighborhood();
            coordinates.Remove(selfCoordinate);
            return coordinates;
        }

        public INeighborhood Clone()
        {
            return new MooreNeighborhood(selfCoordinate.Clone());
        }

        private readonly ICoordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] selfCoordinatesValues;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] lateralOffsetMatrix;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

        private static readonly int[] EmptyOffsetMatrix = Array.Empty<int>();
        private static readonly int[] OffsetMatrix = new int[] { -1, 0, 1 };
    }
}