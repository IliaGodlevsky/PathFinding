using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;

namespace GraphLib.Graphs.Factories
{
    public class Graph3DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph3D(dimensionSizes);
        }
    }
}
