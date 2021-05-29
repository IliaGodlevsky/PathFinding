using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    [Serializable] 
    public sealed class CardinalNeighboursCoordinates : INeighboursCoordinates, ISerializable
    {
        public IEnumerable<ICoordinate> Coordinates => coordinates.Value;

        public CardinalNeighboursCoordinates(ICoordinate coordinate)
        {
            coordinates = new Lazy<IEnumerable<ICoordinate>>(DetectNeighboursCoordinates);
            coordinatesValues = new Coordinate(coordinate);
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

        private IEnumerable<ICoordinate> DetectNeighboursCoordinates()
        {
            return neighboursCoordinates.Coordinates.Where(IsCardinal);
        }

        private readonly Coordinate coordinatesValues;
        private readonly INeighboursCoordinates neighboursCoordinates;
        private readonly Lazy<IEnumerable<ICoordinate>> coordinates;
    }
}