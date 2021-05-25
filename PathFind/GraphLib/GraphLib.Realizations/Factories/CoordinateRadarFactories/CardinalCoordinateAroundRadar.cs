using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.NeighboursCoordinates;

namespace GraphLib.Realizations.Factories.CoordinateRadarFactories
{
    public sealed class CardinalCoordinateAroundRadarFactory : INeighboursCoordinatesFactory
    {
        public INeighboursCoordinates CreateCoordinateRadar(ICoordinate coordinate)
        {
            return new CardinalNeighboursCoordinates(coordinate);
        }
    }
}