using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IGraphAssemble<out TGraph, in TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        TGraph AssembleGraph(IReadOnlyList<int> graphDimensionsSizes);
    }
}
