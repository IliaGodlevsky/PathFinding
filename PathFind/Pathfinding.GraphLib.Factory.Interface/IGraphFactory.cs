using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IGraphFactory<TVertex>
        where TVertex : IVertex
    {
        IGraph<TVertex> CreateGraph(IReadOnlyCollection<TVertex> vertices,
            IReadOnlyList<int> dimensionSizes);
    }
}
