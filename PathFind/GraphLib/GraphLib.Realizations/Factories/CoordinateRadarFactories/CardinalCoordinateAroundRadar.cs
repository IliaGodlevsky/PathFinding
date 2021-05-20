using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.CoordinateRadars;

namespace GraphLib.Realizations.Factories.CoordinateRadarFactories
{
    public sealed class CardinalCoordinateAroundRadarFactory : ICoordinateRadarFactory
    {
        public ICoordinateRadar CreateCoordinateRadar(ICoordinate coordinate)
        {
            return new CardinalCoordinateAroundRadar(coordinate);
        }
    }
}