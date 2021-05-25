namespace GraphLib.Interfaces.Factories
{
    public interface IVertexFactory
    {
        IVertex CreateVertex(INeighboursCoordinates coordinateRadar, ICoordinate coordinate);
    }
}
