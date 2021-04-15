using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Factories
{
    public sealed class Coordinate3DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new Coordinate3D(coordinates.ToArray());
        }
    }
}
