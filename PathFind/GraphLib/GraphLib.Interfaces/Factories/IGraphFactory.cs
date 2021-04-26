namespace GraphLib.Interfaces.Factories
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(int[] dimensionSizes);
    }
}
