namespace GraphLib.Interfaces.Factories
{
    public interface IVertexFactory
    {
        IVertex CreateVertex(INeighboursCoordinates neighboursCoordinates, ICoordinate coordinate);
    }
}
