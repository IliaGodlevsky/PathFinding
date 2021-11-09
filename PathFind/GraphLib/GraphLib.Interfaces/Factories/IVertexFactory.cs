namespace GraphLib.Interfaces.Factories
{
    public interface IVertexFactory
    {
        IVertex CreateVertex(INeighborhood neighboursCoordinates, ICoordinate coordinate);
    }
}
