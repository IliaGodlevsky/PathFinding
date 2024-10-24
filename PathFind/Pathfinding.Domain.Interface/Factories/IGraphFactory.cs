using System.Collections.Generic;

namespace Pathfinding.Domain.Interface.Factories
{
    public interface IGraphFactory<TVertex>
        where TVertex : IVertex
    {
        IGraph<TVertex> CreateGraph(IReadOnlyCollection<TVertex> vertices,
            IReadOnlyList<int> dimensionSizes);
    }
}
