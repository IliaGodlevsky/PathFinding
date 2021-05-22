using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Attributes;

namespace GraphLib.NullRealizations.NullFactories
{
    [Null]
    public sealed class NullGraphAssemble : IGraphAssemble
    {
        public IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionSizes)
        {
            return new NullGraph();
        }
    }
}
