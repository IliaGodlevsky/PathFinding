using GraphLib.Coordinates.Abstractions;
using GraphLib.Coordinates.Infrastructure.Factories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates.Infrastructure.Factories
{
    public sealed class Coordinate3DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new Coordinate3D(coordinates.ToArray());
        }
    }
}
