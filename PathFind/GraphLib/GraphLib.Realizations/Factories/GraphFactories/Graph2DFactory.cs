using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace GraphLib.Realizations.Factories.GraphFactories
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
