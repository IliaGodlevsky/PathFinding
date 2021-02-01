using GraphLib.Graphs.Abstractions;
using System;

namespace GraphLib.Graphs.Factories.Interfaces
{
    public interface IGraphAssembler
    {
        event Action<string> OnExceptionCaught;

        IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
