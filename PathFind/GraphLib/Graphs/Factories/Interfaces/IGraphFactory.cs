using GraphLib.Graphs.Abstractions;
using System;

namespace GraphLib.Graphs.Factories.Interfaces
{
    public interface IGraphFactory
    {
        event Action<string> OnExceptionCaught;

        IGraph CreateGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
