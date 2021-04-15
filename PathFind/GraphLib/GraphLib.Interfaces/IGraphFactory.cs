namespace GraphLib.Interfaces
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(int[] dimensionSizes);
    }
}
