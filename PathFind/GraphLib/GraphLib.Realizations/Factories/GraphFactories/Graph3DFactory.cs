using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace GraphLib.Realizations.Factories.GraphFactories
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
