using System.Collections.Generic;

namespace GraphLib.Interfaces.Factories
{
    public interface IGraphAssemble
    {
        IGraph AssembleGraph(int obstaclePercent, IReadOnlyList<int> graphDimensionSizes);
    }
}
