using Common.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Extensions;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    [Serializable]
    public sealed class CardinalNeighboursCoordinates 
        : INeighboursCoordinates, ISerializable, ICloneable<INeighboursCoordinates>
    {
        public ICoordinate[] Coordinates => coordinates.Value;

        public CardinalNeighboursCoordinates(ICoordinate coordinate)
        {
            coordinates = new Lazy<ICoordinate[]>(DetectNeighboursCoordinates);
            coordinatesValues = coordinate.CoordinatesValues.ToCoordinate();
            neighboursCoordinates = new AroundNeighboursCoordinates(coordinatesValues);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(coordinatesValues), coordinatesValues, typeof(Coordinate));
        }

        private CardinalNeighboursCoordinates(SerializationInfo info, StreamingContext context)
            : this((Coordinate)info.GetValue(nameof(coordinatesValues), typeof(Coordinate)))
        {

        }

        private bool IsCardinal(ICoordinate coordinate)
        {
            return coordinate.IsCardinal(coordinatesValues);
        }

        private ICoordinate[] DetectNeighboursCoordinates()
        {
            return neighboursCoordinates.Coordinates.Where(IsCardinal).ToArray();
        }

        public INeighboursCoordinates Clone()
        {
            return new CardinalNeighboursCoordinates(coordinatesValues);
        }

        private readonly ICoordinate coordinatesValues;
        private readonly INeighboursCoordinates neighboursCoordinates;
        private readonly Lazy<ICoordinate[]> coordinates;
    }
}