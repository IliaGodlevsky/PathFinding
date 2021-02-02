using GraphLib.Graphs;
using GraphLib.Interface;

namespace GraphLib.Factories
{
    public class Graph2DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph2D(dimensionSizes);
        }
    }
}
