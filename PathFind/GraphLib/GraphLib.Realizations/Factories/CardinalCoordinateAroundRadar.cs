using GraphLib.Common.CoordinateRadars;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace GraphLib.Realizations.Factories
{
    public sealed class CardinalCoordinateAroundRadarFactory : ICoordinateRadarFactory
    {
        public ICoordinateRadar CreateCoordinateRadar(ICoordinate coordinate)
        {
            return new CardinalCoordinateAroundRadar(coordinate);
        }
    }
}