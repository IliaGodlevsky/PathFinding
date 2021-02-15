using GraphLib.Realizations;
using GraphLib.Interface;

namespace GraphLib.Realizations.Factories
{
    public sealed class Graph2DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph2D(dimensionSizes);
        }
    }
}
