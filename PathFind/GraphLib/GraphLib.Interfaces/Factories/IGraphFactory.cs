using System.Collections.Generic;

namespace GraphLib.Interfaces.Factories
{
    public interface IGraphFactory<out TGraph, in TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        TGraph CreateGraph(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensionSizes);
    }
}
