namespace GraphLib.Interfaces.Factories
{
    public interface ICoordinateRadarFactory
    {
        ICoordinateRadar CreateCoordinateRadar(ICoordinate coordinate);
    }
}