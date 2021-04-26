using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Coordinates;

namespace GraphLib.Realizations.Factories
{
    public sealed class Coordinate3DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new Coordinate3D(coordinates);
        }
    }
}
