using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.NeighboursCoordinates;

namespace GraphLib.Realizations.Factories.NeighboursCoordinatesFactories
{
    public sealed class CardinalNeighboursCoordinatesFactory : INeighboursCoordinatesFactory
    {
        public INeighboursCoordinates CreateNeighboursCoordinates(ICoordinate coordinate)
        {
            return new CardinalNeighboursCoordinates(coordinate);
        }
    }
}