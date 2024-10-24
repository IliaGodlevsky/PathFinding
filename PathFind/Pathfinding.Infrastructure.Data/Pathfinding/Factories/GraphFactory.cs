using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Data.Pathfinding.Factories
{
    public sealed class GraphFactory<TVertex> : IGraphFactory<TVertex>
        where TVertex : IVertex
    {
        public IGraph<TVertex> CreateGraph(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new Graph<TVertex>(vertices, dimensionSizes);
        }
    }
}
