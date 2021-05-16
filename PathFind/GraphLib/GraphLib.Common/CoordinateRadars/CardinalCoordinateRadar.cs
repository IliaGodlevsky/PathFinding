using GraphLib.Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Common.CoordinateRadars
{
    [Serializable]
    public sealed class CardinalCoordinateAroundRadar : ICoordinateRadar
    {
        public CardinalCoordinateAroundRadar(ICoordinate coordinate)
        {
            coordinatesValues = coordinate.CoordinatesValues.ToArray();
            coordinateRadar = new CoordinateAroundRadar(coordinate);
        }

        public IEnumerable<int[]> Environment => environment ?? FormEnvironment();

        private bool IsCardinal(int[] coordinates)
        {
            return coordinates.IsCardinal(coordinatesValues);
        }

        private IEnumerable<int[]> FormEnvironment()
        {
            return environment = coordinateRadar.Environment.Where(IsCardinal);
        }

        private readonly int[] coordinatesValues;
        private readonly ICoordinateRadar coordinateRadar;
        private IEnumerable<int[]> environment;
    }
}