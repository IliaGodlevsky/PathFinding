using System;

namespace GraphLib.Interface
{
    public interface IGraphAssembler
    {
        event Action<string> OnExceptionCaught;

        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
