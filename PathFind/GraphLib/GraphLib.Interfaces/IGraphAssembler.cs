namespace GraphLib.Interfaces
{
    public interface IGraphAssembler
    {
        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
