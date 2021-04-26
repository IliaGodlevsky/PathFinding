using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Common.CoordinateRadars
{
    public sealed class CardinalCoordinateAroundRadar : ICoordinateRadar
    {
        public CardinalCoordinateAroundRadar(ICoordinate coordinate)
        {
            coordinatesValues = coordinate.CoordinatesValues.ToArray();
            coordinateRadar = new CoordinateAroundRadar(coordinate);
        }

        public IEnumerable<int[]> Environment
            => environment ?? (environment = coordinateRadar.Environment.Where(IsCardinal));

        private bool IsCardinal(int[] coordinates)
        {
            bool IsSubNotZero(int i) => coordinates[i] - coordinatesValues[i] != 0;
            return Enumerable.Range(0, coordinates.Length).Count(IsSubNotZero) == 1;
        }

        private readonly int[] coordinatesValues;
        private readonly ICoordinateRadar coordinateRadar;
        private IEnumerable<int[]> environment;
    }
}