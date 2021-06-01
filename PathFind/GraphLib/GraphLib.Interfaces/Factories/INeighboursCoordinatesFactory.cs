namespace GraphLib.Interfaces.Factories
{
    public interface INeighboursCoordinatesFactory
    {
        INeighboursCoordinates CreateNeighboursCoordinates(ICoordinate coordinate);
    }
}