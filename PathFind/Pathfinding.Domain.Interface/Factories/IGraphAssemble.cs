using System.Collections.Generic;

namespace Pathfinding.Domain.Interface.Factories
{
    public interface IGraphAssemble<TVertex>
        where TVertex : IVertex
    {
        IGraph<TVertex> AssembleGraph(IReadOnlyList<int> graphDimensionsSizes);
    }
}
