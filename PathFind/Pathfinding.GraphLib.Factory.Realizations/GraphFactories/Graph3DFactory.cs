using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Realizations.GraphFactories
{
    public sealed class Graph3DFactory<TVertex> : IGraphFactory<Graph3D<TVertex>, TVertex>
        where TVertex : IVertex
    {
        public Graph3D<TVertex> CreateGraph(IReadOnlyCollection<TVertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new Graph3D<TVertex>(vertices, dimensionSizes);
        }
    }
}
