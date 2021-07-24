namespace GraphLib.Interfaces.Factories
{
    public interface IGraphAssemble
    {
        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
