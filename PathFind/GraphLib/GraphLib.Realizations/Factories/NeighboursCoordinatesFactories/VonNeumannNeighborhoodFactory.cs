using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.NeighboursCoordinates;

namespace GraphLib.Realizations.Factories.NeighboursCoordinatesFactories
{
    public sealed class VonNeumannNeighborhoodFactory : INeighborhoodFactory
    {
        public INeighborhood CreateNeighborhood(ICoordinate coordinate)
        {
            return new VonNeumannNeighborhood(coordinate);
        }
    }
}