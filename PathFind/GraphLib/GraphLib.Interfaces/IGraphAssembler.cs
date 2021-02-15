using System;

namespace GraphLib.Interface
{
    public interface IGraphAssembler
    {
        event Action<Exception> OnExceptionCaught;

        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
