using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.NeighboursCoordinates
{
    [Serializable]
    public sealed class CardinalNeighboursCoordinates : INeighboursCoordinates
    {
        public CardinalNeighboursCoordinates(ICoordinate coordinate)
        {
            coordinatesValues = new Coordinate(coordinate);
            neighboursCoordinates = new AroundNeighboursCoordinates(coordinate);
        }

        public IEnumerable<ICoordinate> Coordinates => coordinates ?? FormNeighboursCoordinates();

        private bool IsCardinal(ICoordinate coordinate)
        {
            return coordinate.IsCardinal(coordinatesValues);
        }

        private IEnumerable<ICoordinate> FormNeighboursCoordinates()
        {
            return coordinates = neighboursCoordinates.Coordinates.Where(IsCardinal);
        }

        private readonly Coordinate coordinatesValues;
        private readonly INeighboursCoordinates neighboursCoordinates;

        [NonSerialized]
        private IEnumerable<ICoordinate> coordinates;
    }
}