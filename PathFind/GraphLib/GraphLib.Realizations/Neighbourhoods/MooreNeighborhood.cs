using Common.Extensions;
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
    public sealed class MooreNeighborhood : INeighborhood, ISerializable
    {
        private static readonly int[] EmptyOffsetMatrix = Array.Empty<int>();
        private static readonly int[] OffsetMatrix = new int[] { -1, 0, 1 };

        private readonly ICoordinate selfCoordinate;
        private readonly int limitDepth;
        private readonly int[] selfCoordinatesValues;
        private readonly int[] resultCoordinatesValues;
        private readonly int[] lateralOffsetMatrix;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;

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

        public INeighborhood Clone()
        {
            return new MooreNeighborhood(selfCoordinate.Clone());
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private List<ICoordinate> DetectNeighborhood(int depth = 0)
        {
            var neighborhood = new List<ICoordinate>();
            foreach (int offset in lateralOffsetMatrix)
            {
                resultCoordinatesValues[depth] = selfCoordinatesValues[depth] + offset;
                neighborhood.AddRange(GetCoordinates(depth));
            }
            return neighborhood;
        }

        private IReadOnlyCollection<ICoordinate> GetCoordinates(int depth)
        {
            return depth < limitDepth - 1
                ? (IReadOnlyCollection<ICoordinate>)DetectNeighborhood(depth + 1)
                : new[] { resultCoordinatesValues.ToCoordinate() };
        }

        private List<ICoordinate> GetNeighborhood()
        {
            var coordinates = DetectNeighborhood();
            coordinates.Remove(selfCoordinate);
            return coordinates;
        }
    }
}