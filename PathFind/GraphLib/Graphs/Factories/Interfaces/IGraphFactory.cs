using GraphLib.Graphs.Abstractions;
using System;

namespace GraphLib.Graphs.Factories.Interfaces
{
    public interface IGraphFiller
    {
        event Action<string> OnExceptionCaught;

        IGraph CreateGraph(int obstaclePercent, params int[] graphDimensionSizes);
    }
}
