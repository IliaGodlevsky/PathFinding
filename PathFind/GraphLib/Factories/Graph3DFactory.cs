using GraphLib.Graphs;
using GraphLib.Interface;

namespace GraphLib.Factories
{
    public class Graph3DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph3D(dimensionSizes);
        }
    }
}
