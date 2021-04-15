using GraphLib.Interfaces;

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
