namespace GraphLib.Interfaces.Factories
{
    public interface ICoordinateFactory
    {
        ICoordinate CreateCoordinate(int[] coordinates);
    }
}
