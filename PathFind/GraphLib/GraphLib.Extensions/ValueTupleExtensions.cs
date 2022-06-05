using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace GraphLib.Extensions
{
    public static class ValueTupleExtensions
    {
        public static IVertex Create(this (ICoordinateFactory CoordinateFactory,
            INeighborhoodFactory NeighbourhoodFactory, IVertexFactory VertexFactory) factories, int[] coordinates)
        {
            var coordinate = factories.CoordinateFactory.CreateCoordinate(coordinates);
            var neighbourhood = factories.NeighbourhoodFactory.CreateNeighborhood(coordinate);
            return factories.VertexFactory.CreateVertex(neighbourhood, coordinate);
        }
    }
}
