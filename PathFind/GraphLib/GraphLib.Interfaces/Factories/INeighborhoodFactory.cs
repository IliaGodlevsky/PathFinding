namespace GraphLib.Interfaces.Factories
{
    public interface INeighborhoodFactory
    {
        INeighborhood CreateNeighborhood(ICoordinate coordinate);
    }
}