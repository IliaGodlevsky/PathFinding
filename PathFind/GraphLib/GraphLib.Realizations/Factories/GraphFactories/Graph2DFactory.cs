using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;

namespace GraphLib.Realizations.Factories.GraphFactories
{
    public sealed class Graph2DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph2D(dimensionSizes);
        }
    }
}
