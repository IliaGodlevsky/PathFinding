using Common.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    [Serializable]
    public sealed class CardinalNeighboursCoordinates : INeighboursCoordinates, ISerializable, ICloneable<INeighboursCoordinates>
    {
        public IReadOnlyCollection<ICoordinate> Coordinates => coordinates.Value;

        public CardinalNeighboursCoordinates(ICoordinate coordinate)
        {
            coordinates = new Lazy<IReadOnlyCollection<ICoordinate>>(DetectNeighboursCoordinates);
            selfCoordinate = coordinate;
            neighboursCoordinates = new AroundNeighboursCoordinates(coordinate);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(selfCoordinate), selfCoordinate, typeof(ICoordinate));
        }

        private CardinalNeighboursCoordinates(SerializationInfo info, StreamingContext context)
            : this((ICoordinate)info.GetValue(nameof(selfCoordinate), typeof(ICoordinate)))
        {

        }

        private bool IsCardinal(ICoordinate coordinate)
        {
            return coordinate.IsCardinal(selfCoordinate);
        }

        private IReadOnlyCollection<ICoordinate> DetectNeighboursCoordinates()
        {
            return neighboursCoordinates.Coordinates.Where(IsCardinal).ToArray();
        }

        public INeighboursCoordinates Clone()
        {
            return new CardinalNeighboursCoordinates(selfCoordinate.Clone());
        }

        private readonly ICoordinate selfCoordinate;
        private readonly INeighboursCoordinates neighboursCoordinates;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> coordinates;
    }
}