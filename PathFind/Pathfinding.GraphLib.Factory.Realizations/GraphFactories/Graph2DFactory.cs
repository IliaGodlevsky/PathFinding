using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Realizations.GraphFactories
{
    public sealed class Graph2DFactory<TVertex> : IGraphFactory<Graph2D<TVertex>, TVertex>
        where TVertex : IVertex
    {
        public Graph2D<TVertex> CreateGraph(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new Graph2D<TVertex>(vertices, dimensionSizes);
        }
    }
}
