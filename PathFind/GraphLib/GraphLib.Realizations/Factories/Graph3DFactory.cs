using GraphLib.Realizations;
using GraphLib.Interface;

namespace GraphLib.Realizations.Factories
{
    public sealed class Graph3DFactory : IGraphFactory
    {
        public IGraph CreateGraph(int[] dimensionSizes)
        {
            return new Graph3D(dimensionSizes);
        }
    }
}
