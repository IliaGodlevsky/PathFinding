using GraphLib.Common.CoordinateRadars;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace GraphLib.Realizations.Factories.CoordinateRadarFactories
{
    public sealed class CoordinateAroundRadarFactory : ICoordinateRadarFactory
    {
        public ICoordinateRadar CreateCoordinateRadar(ICoordinate coordinate)
        {
            return new CoordinateAroundRadar(coordinate);
        }
    }
}