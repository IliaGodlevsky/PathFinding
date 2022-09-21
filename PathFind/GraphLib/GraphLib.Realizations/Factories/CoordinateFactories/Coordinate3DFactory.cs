using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;

namespace GraphLib.Realizations.Factories.CoordinateFactories
{
    public sealed class Coordinate3DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates)
        {
            return new Coordinate3D(coordinates);
        }
    }
}
