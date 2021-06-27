using AssembleClassesLib.Attributes;

namespace GraphLib.Interfaces.Factories
{
    [NotLoadable]
    public interface IGraphAssemble
    {
        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
