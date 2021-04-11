namespace GraphLib.Interface
{
    public interface IGraphAssembler
    {
        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
