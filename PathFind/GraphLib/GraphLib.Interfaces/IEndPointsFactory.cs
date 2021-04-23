namespace GraphLib.Interfaces
{
    public interface IEndPointsFactory
    {
        IEndPoints CreateEndPoints(IGraph graph);
    }
}