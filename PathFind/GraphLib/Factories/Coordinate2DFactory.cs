using GraphLib.Interface;
using GraphLib.NullObjects;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Factories
{
    public sealed class Coordinate2DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IEnumerable<int> coordinates)
        {
            return new Coordinate2D(coordinates.ToArray());
        }
    }
}
