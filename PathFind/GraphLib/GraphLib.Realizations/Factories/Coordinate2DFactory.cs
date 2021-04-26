using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Coordinates;

namespace GraphLib.Realizations.Factories
{
    public sealed class Coordinate2DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(int[] coordinates)
        {
            return new Coordinate2D(coordinates);
        }
    }
}
