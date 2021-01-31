using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;

namespace GraphLib.Graphs.Factories
{
    public class Graph2DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph2D(dimensionSizes);
        }
    }
}
