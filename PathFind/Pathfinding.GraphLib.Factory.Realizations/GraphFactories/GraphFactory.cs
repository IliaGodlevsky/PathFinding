using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Realizations.GraphFactories
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
