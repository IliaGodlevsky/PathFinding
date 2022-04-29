using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace GraphLib.Realizations.Factories.GraphFactories
{
    public sealed class Graph3DFactory : IGraphFactory
    {
        public IGraph CreateGraph(IReadOnlyCollection<IVertex> vertices, int[] dimensionSizes)
        {
            return new Graph3D(vertices, dimensionSizes);
        }
    }
}
