namespace GraphLib.Interfaces.Factories
{
    public interface IGraphAssembler
    {
        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
