using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IGraphFactory<out TGraph, in TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        TGraph CreateGraph(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensionSizes);
    }
}
