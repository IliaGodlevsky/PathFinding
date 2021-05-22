using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.CoordinateRadars
{
    [Serializable]
    public sealed class CardinalCoordinateAroundRadar : ICoordinateRadar
    {
        public CardinalCoordinateAroundRadar(ICoordinate coordinate)
        {
            coordinatesValues = new Coordinate(coordinate);
            coordinateRadar = new CoordinateAroundRadar(coordinate);
        }

        public IEnumerable<ICoordinate> Environment => environment ?? FormEnvironment();

        private bool IsCardinal(ICoordinate coordinate)
        {
            return coordinate.IsCardinal(coordinatesValues);
        }

        private IEnumerable<ICoordinate> FormEnvironment()
        {
            return environment = coordinateRadar.Environment.Where(IsCardinal);
        }

        private readonly Coordinate coordinatesValues;
        private readonly ICoordinateRadar coordinateRadar;

        [NonSerialized]
        private IEnumerable<ICoordinate> environment;
    }
}