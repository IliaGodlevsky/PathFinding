namespace GraphLib.Interfaces.Factories
{
    public interface INeighboursCoordinatesFactory
    {
        INeighboursCoordinates CreateCoordinateRadar(ICoordinate coordinate);
    }
}