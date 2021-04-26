namespace GraphLib.Interfaces.Factories
{
    public interface IVertexFactory
    {
        IVertex CreateVertex(ICoordinateRadar coordinateRadar, ICoordinate coordinate);
    }
}
