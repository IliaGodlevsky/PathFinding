using Common.Extensions;
using Common.Interface;
using GraphLib.Interfaces;
using GraphLib.Proxy.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.Neighbourhoods
{
    [Serializable]
    [DebuggerDisplay("Count = {Neighbours.Length}")]
    public sealed class MooreNeighborhood : INeighborhood, ISerializable, ICloneable<INeighborhood>
    {
        public IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public MooreNeighborhood(ICoordinate coordinate)
        {
            selfCoordinate = coordinate;
            selfCoordinatesValues = selfCoordinate.CoordinatesValues.ToArray();
            limitDepth = selfCoordinatesValues.Length;
            resultCoordinatesValues = new int[limitDepth];
            lateralOffsetMatrix = limitDepth == 0 ? EmptyOffsetMatrix : OffsetMatrix;
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(GetNeighborhood);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.Add(nameof(selfCoordinate), selfCoordinate);
        }

        private MooreNeighborhood(SerializationInfo info, StreamingContext context)
            : this(info.Get<ICoordinate>(nameof(selfCoordinate)))
        {

        }
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

        object ICloneable.Clone()
        {
            return Clone();
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