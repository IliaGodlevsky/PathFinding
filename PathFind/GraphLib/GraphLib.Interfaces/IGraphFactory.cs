namespace GraphLib.Interface
{
    public interface IGraphFactory
    {
        IGraph CreateGraph(int[] dimensionSizes);
    }
}
