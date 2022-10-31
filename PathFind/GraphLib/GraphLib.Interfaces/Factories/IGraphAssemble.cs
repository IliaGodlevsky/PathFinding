using System.Collections.Generic;

namespace GraphLib.Interfaces.Factories
{
    public interface IGraphAssemble<out TGraph, in TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        TGraph AssembleGraph(int obstaclePercent, IReadOnlyList<int> graphDimensionSizes);
    }
}
