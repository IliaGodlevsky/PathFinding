using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;

namespace GraphLib.Realizations.Factories.CoordinateFactories
{
    public sealed class Coordinate2DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates)
        {
            return new Coordinate2D(coordinates);
        }
    }
}
